using Microsoft.EntityFrameworkCore;
using OnlineSouvenirShopAPI.Data;
using OnlineSouvenirShopAPI.DTOs;
using OnlineSouvenirShopAPI.Models;
using OnlineSouvenirShopAPI.Repositories.Implementations;

namespace OnlineSouvenirShopAPI.Repositories.Interfaces
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ProductRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            return await _dbContext.Products.ToListAsync();
        }

        public async Task<Product?> GetOne(Guid id)
        {
            return await _dbContext.Products.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task Create(Product product)
        {
            _dbContext.Products.Add(product);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Product> Update(Product product)
        {
            _dbContext.Products.Update(product);
            await _dbContext.SaveChangesAsync();
            return product;
        }

        public async Task<Product> Delete(Guid id)
        {
            var product = await _dbContext.Products.FirstOrDefaultAsync(x => x.Id == id);
            if (product == null)
            {
                throw new Exception("Product not found");
            }
            _dbContext.Products.Remove(product);
            await _dbContext.SaveChangesAsync();
            return product;
        }
    }
}
