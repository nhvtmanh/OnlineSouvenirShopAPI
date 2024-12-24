using OnlineSouvenirShopAPI.DTOs.OrderDTOs;
using OnlineSouvenirShopAPI.Models;

namespace OnlineSouvenirShopAPI.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        Task<Order> Create(Order order);
        Task<IEnumerable<Order>> GetAll();
        Task<Order?> GetOne(Guid id);
        Task<IEnumerable<Order>> SearchOrders(OrderQueryObject query);
        Task<IEnumerable<Order>> FilterOrders(byte status);
        Task<Order> Update(Order order);
    }
}
