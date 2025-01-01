using OnlineSouvenirShopAPI.Data;
using OnlineSouvenirShopAPI.Models;
using OnlineSouvenirShopAPI.Repositories.Interfaces;

namespace OnlineSouvenirShopAPI.Repositories.Implementations
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public CommentRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Review> Create(Review review)
        {
            _dbContext.Reviews.Add(review);
            await _dbContext.SaveChangesAsync();
            return review;
        }
    }
}
