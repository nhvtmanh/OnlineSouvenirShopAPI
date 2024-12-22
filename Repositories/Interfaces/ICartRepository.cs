using OnlineSouvenirShopAPI.Models;

namespace OnlineSouvenirShopAPI.Repositories.Interfaces
{
    public interface ICartRepository
    {
        //Task<IEnumerable<CartItem>> GetAllCartItems();
        Task<Cart?> GetOneCart(Guid customerId);
        Task<Cart> CreateCart(Guid customerId);
        Task<CartItem> CreateCartItem(Guid cartId, Guid productId, int quantity);
        //Task<Category> Update(Category category);
        Task<CartItem> UpdateCartItem(CartItem cartItem);
        //Task<Category?> Delete(Guid id);
    }
}
