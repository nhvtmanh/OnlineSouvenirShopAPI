using OnlineSouvenirShopAPI.DTOs.OrderDTOs;
using OnlineSouvenirShopAPI.Models;

namespace OnlineSouvenirShopAPI.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        Task<Order> Create(Order order);
        Task<IEnumerable<Order>> GetAll();
        Task<IEnumerable<Order>> SearchOrders(OrderQueryObject query);
    }
}
