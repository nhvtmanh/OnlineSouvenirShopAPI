using OnlineSouvenirShopAPI.Models;

namespace OnlineSouvenirShopAPI.Repositories.Interfaces
{
    public interface ICommentRepository
    {
        Task<Review> Create(Review review);
        Task<Review> GetOne(Guid id);
    }
}
