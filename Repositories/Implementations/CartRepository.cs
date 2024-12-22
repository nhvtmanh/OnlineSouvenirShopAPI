using Microsoft.EntityFrameworkCore;
using OnlineSouvenirShopAPI.Data;
using OnlineSouvenirShopAPI.Models;
using OnlineSouvenirShopAPI.Repositories.Interfaces;

namespace OnlineSouvenirShopAPI.Repositories.Implementations
{
    public class CartRepository : ICartRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public CartRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Cart> CreateCart(Guid customerId)
        {
            var cart = new Cart
            {
                CustomerId = customerId
            };
            _dbContext.Carts.Add(cart);
            await _dbContext.SaveChangesAsync();
            return cart;
        }

        public async Task<CartItem> CreateCartItem(Guid cartId, Guid productId, int quantity)
        {
            var cartItem = new CartItem
            {
                CartId = cartId,
                ProductId = productId,
                Quantity = quantity
            };
            _dbContext.CartItems.Add(cartItem);
            await _dbContext.SaveChangesAsync();
            return cartItem;
        }

        public async Task<Cart?> GetOneCart(Guid customerId)
        {
            return await _dbContext.Carts.Include(c => c.CartItems).FirstOrDefaultAsync(x => x.CustomerId == customerId);
        }

        public async Task<CartItem> UpdateCartItem(CartItem cartItem)
        {
            _dbContext.CartItems.Update(cartItem);
            await _dbContext.SaveChangesAsync();
            return cartItem;
        }
    }
}
