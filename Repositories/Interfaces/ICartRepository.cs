using OnlineSouvenirShopAPI.Models;

namespace OnlineSouvenirShopAPI.Repositories.Interfaces
{
    public interface ICartRepository
    {
        Task<Cart?> GetOneCart(Guid customerId);
        Task<IEnumerable<CartItem>> GetCartItems(List<Guid> cartItemIds);
        Task<Cart> CreateCart(Guid customerId);
        Task<CartItem> CreateCartItem(Guid cartId, Guid productId, int quantity);
        Task<CartItem> UpdateCartItem(CartItem cartItem);
        Task DeleteCartItems(List<Guid> productIds);
    }
}
