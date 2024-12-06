using OnlineSouvenirShopAPI.DTOs;
using OnlineSouvenirShopAPI.Models;

namespace OnlineSouvenirShopAPI.Repositories.Implementations
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAll();
        Task<Product?> GetOne(Guid id);
        Task Create(Product product);
        Task<Product> Update(Product product);
        Task<Product> Delete(Guid id);
    }
}
