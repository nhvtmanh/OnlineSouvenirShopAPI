using OnlineSouvenirShopAPI.DTOs;
using OnlineSouvenirShopAPI.Models;

namespace OnlineSouvenirShopAPI.Repositories.Implementations
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAll();
        Task<Category?> GetOne(Guid id);
        Task Create(Category category);
        Task<Category> Update(Category category);
        Task<Category> Delete(Guid id);
    }
}
