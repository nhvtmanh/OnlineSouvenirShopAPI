using OnlineSouvenirShopAPI.Models;

namespace OnlineSouvenirShopAPI.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        Task<Order> Create(Order order);
    }
}
