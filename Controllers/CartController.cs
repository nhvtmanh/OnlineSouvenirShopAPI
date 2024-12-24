using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineSouvenirShopAPI.DTOs;
using OnlineSouvenirShopAPI.Repositories.Implementations;
using OnlineSouvenirShopAPI.Repositories.Interfaces;
using System.Security.Claims;

namespace OnlineSouvenirShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Customer")]
    public class CartController : ControllerBase
    {
        private readonly ICartRepository _cartRepository;

        public CartController(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        [HttpGet("get-cart")]
        public async Task<IActionResult> GetCart()
        {
            var customerId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var cart = await _cartRepository.GetOneCart(customerId);
            return Ok(cart);
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart([FromBody] AddToCartDTO addToCartDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Check if customer has a cart
            var customerId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            var cart = await _cartRepository.GetOneCart(customerId);

            if (cart == null)
            {
                // Create a new cart
                var newCart = await _cartRepository.CreateCart(customerId);
                // Add item to cart
                var newCartItem = await _cartRepository.CreateCartItem(newCart.Id, addToCartDTO.ProductId, addToCartDTO.Quantity);
                return Ok(newCartItem);
            }

            // Check if item is already in cart
            var item = cart.CartItems.FirstOrDefault(x => x.ProductId == addToCartDTO.ProductId);
            if (item != null)
            {
                item.Quantity += addToCartDTO.Quantity;
                item = await _cartRepository.UpdateCartItem(item);
                return Ok(item);
            }

            // Add item to cart
            var cartItem = await _cartRepository.CreateCartItem(cart.Id, addToCartDTO.ProductId, addToCartDTO.Quantity);
            return Ok(cartItem);
        }
    }
}
