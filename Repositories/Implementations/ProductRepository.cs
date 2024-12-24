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

        public async Task<Product?> Delete(Guid id)
        {
            var product = await _dbContext.Products.FirstOrDefaultAsync(x => x.Id == id);
            if (product == null)
            {
                return null;
            }
            _dbContext.Products.Remove(product);
            await _dbContext.SaveChangesAsync();
            return product;
        }

        public async Task<IEnumerable<Product>> GetByName(string name)
        {
            return await _dbContext.Products
                .Where(x => x.Name.ToLower().Contains(name.ToLower()))
                .ToListAsync();
        }

        public async Task<FavoriteProduct?> AddFavorite(Guid customerId, Guid productId)
        {
            if (!_dbContext.FavoriteProducts.Any(x => x.CustomerId == customerId && x.ProductId == productId))
            {
                var favoriteProduct = new FavoriteProduct
                {
                    CustomerId = customerId,
                    ProductId = productId
                };
                _dbContext.FavoriteProducts.Add(favoriteProduct);
                await _dbContext.SaveChangesAsync();
                return favoriteProduct;
            }
            return null;
        }

        public async Task<FavoriteProduct?> RemoveFavorite(Guid customerId, Guid productId)
        {
            var favoriteProduct = await _dbContext.FavoriteProducts
                .FirstOrDefaultAsync(x => x.CustomerId == customerId && x.ProductId == productId);

            if (favoriteProduct != null)
            {
                _dbContext.FavoriteProducts.Remove(favoriteProduct);
                await _dbContext.SaveChangesAsync();
                return favoriteProduct;
            }
            return null;
        }

        public async Task<IEnumerable<FavoriteProduct>> GetFavorite(Guid customerId)
        {
            return await _dbContext.FavoriteProducts
                .Include(x => x.Product)
                .Where(x => x.CustomerId == customerId)
                .ToListAsync();
        }
    }
}
