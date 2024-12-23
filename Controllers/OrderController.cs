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

        public OrderController(UserManager<AppUser> userManager, ICartRepository cartRepository, IOrderRepository orderRepository, IProductRepository productRepository)
        {
            _userManager = userManager;
            _cartRepository = cartRepository;
            _orderRepository = orderRepository;
            _productRepository = productRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var orders = await _orderRepository.GetAll();
            return Ok(orders);
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchOrders([FromQuery] OrderQueryObject orderQueryObject)
        {
            var orders = await _orderRepository.SearchOrders(orderQueryObject);
            return Ok(orders);
        }

        [HttpPost("purchase")]
        public async Task<IActionResult> Purchase([FromBody] PurchaseDTO purchaseDTO)
        {
            var user = await _userManager.FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var cartItems = await _cartRepository.GetCartItems(purchaseDTO.CartItemIds);

            if (cartItems.Count() == 0)
            {
                return BadRequest(new { message = "No cart items found" });
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

            return Ok(order);
        }

        [HttpPost("place-order")]
        public async Task<IActionResult> PlaceOrder([FromBody] Order order)
        {
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
            await _cartRepository.DeleteCartItems(productIds);

            return Ok(createdOrder);
        }
    }
}
