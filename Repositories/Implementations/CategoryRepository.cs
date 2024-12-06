using Microsoft.EntityFrameworkCore;
using OnlineSouvenirShopAPI.Data;
using OnlineSouvenirShopAPI.DTOs;
using OnlineSouvenirShopAPI.Models;
using OnlineSouvenirShopAPI.Repositories.Implementations;

namespace OnlineSouvenirShopAPI.Repositories.Interfaces
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public CategoryRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Category>> GetAll()
        {
            return await _dbContext.Categories.ToListAsync();
        }

        public async Task<Category?> GetOne(Guid id)
        {
            return await _dbContext.Categories.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task Create(Category category)
        {
            _dbContext.Categories.Add(category);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Category> Update(Category category)
        {
            _dbContext.Categories.Update(category);
            await _dbContext.SaveChangesAsync();
            return category;
        }

        public async Task<Category> Delete(Guid id)
        {
            var category = await _dbContext.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if (category == null)
            {
                throw new Exception("Category not found");
            }
            _dbContext.Categories.Remove(category);
            await _dbContext.SaveChangesAsync();
            return category;
        }
    }
}
