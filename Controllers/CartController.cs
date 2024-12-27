using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineSouvenirShopAPI.DTOs;
using OnlineSouvenirShopAPI.DTOs.CartDTOs;
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

        [HttpPut]
        public async Task<IActionResult> UpdateCartItem([FromBody] UpdateCartItemDTO updateCartItemDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var cartItem = await _cartRepository.GetCartItem(updateCartItemDTO.Id);
            if (cartItem == null)
            {
                return NotFound(new { message = "Cart item not found" });
            }

            cartItem.Quantity = updateCartItemDTO.Quantity;
            await _cartRepository.UpdateCartItem(cartItem);

            return Ok(cartItem);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCartItems([FromBody] DeleteCartItemDTO deleteCartItemDTO)
        {
            var cartItems = await _cartRepository.GetCartItems(deleteCartItemDTO.CartItemIds);
            if (cartItems.Count() == 0)
            {
                return NotFound(new { message = "No cart items found" });
            }

            await _cartRepository.DeleteCartItems(cartItems);
            return Ok(cartItems);
        }
    }
}
