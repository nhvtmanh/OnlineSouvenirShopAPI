using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineSouvenirShopAPI.DTOs;
using OnlineSouvenirShopAPI.DTOs.OrderDTOs;
using OnlineSouvenirShopAPI.Helpers.Enums;
using OnlineSouvenirShopAPI.Models;
using OnlineSouvenirShopAPI.Repositories.Implementations;
using OnlineSouvenirShopAPI.Repositories.Interfaces;
using System.Security.Claims;

namespace OnlineSouvenirShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrderController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ICartRepository _cartRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;
        private readonly IVoucherRepository _voucherRepository;

        public OrderController(UserManager<AppUser> userManager, ICartRepository cartRepository, IOrderRepository orderRepository, IProductRepository productRepository, IVoucherRepository voucherRepository)
        {
            _userManager = userManager;
            _cartRepository = cartRepository;
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _voucherRepository = voucherRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var orders = await _orderRepository.GetAll();
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(Guid id)
        {
            var order = await _orderRepository.GetOne(id);
            if (order == null)
            {
                return NotFound(new { message = "Order not found" });
            }
            return Ok(order);
        }

        [HttpGet("get-customer-orders")]
        public async Task<IActionResult> GetCustomerOrders()
        {
            var customerId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var orders = await _orderRepository.GetCustomerOrders(customerId);
            return Ok(orders);
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchOrders([FromQuery] OrderQueryObject orderQueryObject)
        {
            var orders = await _orderRepository.SearchOrders(orderQueryObject);
            return Ok(orders);
        }

        [HttpGet("filter")]
        public async Task<IActionResult> FilterOrders([FromQuery] byte status)
        {
            var orders = await _orderRepository.FilterOrders(status);
            return Ok(orders);
        }

        [HttpPost("purchase")]
        public async Task<IActionResult> Purchase([FromBody] PurchaseDTO purchaseDTO)
        {
            var user = await _userManager.FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var cartItems = await _cartRepository.GetCartItems(purchaseDTO.CartItemIds);

            if (cartItems.Count() == 0)
            {
                return NotFound(new { message = "No cart items found" });
            }

            // Check quantity in stock
            foreach (var item in cartItems)
            {
                if (item.Quantity > item.Product!.StockQuantity)
                {
                    return BadRequest(new { message = "Not enough stock" });
                }
            }

            var order = new Order
            {
                Id = Guid.NewGuid(),
                OrderDate = DateTime.Now,
                Status = (byte)OrderStatus.Pending,
                Total = cartItems.Sum(x => x.Product!.DiscountPrice * x.Quantity),
                CustomerId = user!.Id,
                OrderItems = cartItems.Select(x => new OrderItem
                {
                    Id = Guid.NewGuid(),
                    ProductId = x.ProductId,
                    Quantity = x.Quantity
                }).ToList()
            };

            // Apply voucher code
            if (!string.IsNullOrEmpty(purchaseDTO.VoucherCode))
            {
                var voucher = await _voucherRepository.GetByCode(purchaseDTO.VoucherCode);

                // Check if voucher exists
                if (voucher == null)
                {
                    return NotFound(new { message = "Voucher not found" });
                }

                // Check if voucher is active
                if (voucher.Status != (byte)VoucherStatus.Active)
                {
                    return BadRequest(new { message = "Voucher cannot be used" });
                }

                // Check if current usage is less than max usage
                if (voucher.CurrentUsageCount >= voucher.MaxUsageCount)
                {
                    return BadRequest(new { message = "Voucher has reached max usage" });
                }

                order.VoucherId = voucher.Id;
                order.Total = order.Total * (1 - (decimal)voucher.DiscountAmount / 100);
            }

            return Ok(order);
        }

        [HttpPost("place-order")]
        public async Task<IActionResult> PlaceOrder([FromBody] Order order)
        {
            // Check voucher
            if (order.VoucherId != null)
            {
                var voucher = await _voucherRepository.GetOne((Guid)order.VoucherId);
                if (voucher == null)
                {
                    return NotFound(new { message = "Voucher not found" });
                }

                // Check if voucher is active
                if (voucher.Status != (byte)VoucherStatus.Active)
                {
                    return BadRequest(new { message = "Voucher cannot be used" });
                }

                // Check if current usage is less than max usage
                if (voucher.CurrentUsageCount >= voucher.MaxUsageCount)
                {
                    return BadRequest(new { message = "Voucher has reached max usage" });
                }

                voucher.CurrentUsageCount++;
                await _voucherRepository.Update(voucher);
            }

            order.OrderDate = DateTime.Now;
            order.Status = (byte)OrderStatus.Processing;

            List<Guid> productIds = new List<Guid>();
            foreach (var item in order.OrderItems)
            {
                item.OrderId = order.Id;
                productIds.Add((Guid)item.ProductId!);
            }

            // Create order
            var createdOrder = await _orderRepository.Create(order);

            // Update stock quantity
            foreach (var item in order.OrderItems)
            {
                var product = await _productRepository.GetOne((Guid)item.ProductId!);
                product!.StockQuantity -= item.Quantity;
                await _productRepository.Update(product);
            }

            // Delete cart items
            await _cartRepository.DeleteCartItemsByProductIds(productIds);

            return Ok(createdOrder);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrderStatus(Guid id, [FromBody] byte status)
        {
            var order = await _orderRepository.GetOne(id);
            if (order == null)
            {
                return NotFound(new { message = "Order not found" });
            }

            order.Status = status;
            await _orderRepository.Update(order);

            // Check if order is delivered -> Update sold quantity
            if (status == (byte)OrderStatus.Delivered)
            {
                foreach (var item in order.OrderItems)
                {
                    var product = await _productRepository.GetOne((Guid)item.ProductId!);
                    product!.SoldQuantity += item.Quantity;
                    await _productRepository.Update(product);
                }
            }

            return Ok(order);
        }
    }
}
