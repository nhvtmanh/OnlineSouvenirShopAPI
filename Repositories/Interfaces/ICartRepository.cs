using OnlineSouvenirShopAPI.Models;

namespace OnlineSouvenirShopAPI.Repositories.Interfaces
{
    public interface ICartRepository
    {
        Task<Cart?> GetOneCart(Guid customerId);
        Task<CartItem?> GetCartItem(Guid cartItemId);
        Task<IEnumerable<CartItem>> GetCartItems(List<Guid> cartItemIds);
        Task<Cart> CreateCart(Guid customerId);
        Task<CartItem> CreateCartItem(Guid cartId, Guid productId, int quantity);
        Task<CartItem> UpdateCartItem(CartItem cartItem);
        Task DeleteCartItemsByProductIds(List<Guid> productIds);
        Task DeleteCartItems(IEnumerable<CartItem> cartItems);
    }
}
