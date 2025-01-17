﻿using OnlineSouvenirShopAPI.DTOs;
using OnlineSouvenirShopAPI.DTOs.ProductDTOs;
using OnlineSouvenirShopAPI.Models;

namespace OnlineSouvenirShopAPI.Repositories.Implementations
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAll(PagingQueryObject queryObject);
        Task<Product?> GetOne(Guid id);
        Task<IEnumerable<Product>> GetByName(string name);
        Task Create(Product product);
        Task<Product> Update(Product product);
        Task<Product?> Delete(Guid id);
        Task<FavoriteProduct?> AddFavorite(Guid customerId, Guid productId);
        Task<FavoriteProduct?> RemoveFavorite(Guid customerId, Guid productId);
        Task<IEnumerable<FavoriteProduct>> GetFavorite(Guid customerId);
        Task<IEnumerable<Product>> GetByCategory(string name);
        Task<ProductDashboardResponse> GetProductDashboard();
    }
}
