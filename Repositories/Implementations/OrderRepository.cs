using Microsoft.EntityFrameworkCore;
using OnlineSouvenirShopAPI.Data;
using OnlineSouvenirShopAPI.DTOs.OrderDTOs;
using OnlineSouvenirShopAPI.Models;
using OnlineSouvenirShopAPI.Repositories.Interfaces;

namespace OnlineSouvenirShopAPI.Repositories.Implementations
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public OrderRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Order> Create(Order order)
        {
            _dbContext.OrderItems.AddRange(order.OrderItems);
            _dbContext.Orders.Add(order);
            await _dbContext.SaveChangesAsync();
            return order;
        }

        public async Task<IEnumerable<Order>> FilterOrders(byte status)
        {
            return await _dbContext.Orders.Where(x => x.Status == status).ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetAll()
        {
            return await _dbContext.Orders.ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetCustomerOrders(Guid customerId)
        {
            return await _dbContext.Orders.Include(o => o.OrderItems).Where(x => x.CustomerId == customerId).ToListAsync();
        }

        public async Task<Order?> GetOne(Guid id)
        {
            return await _dbContext.Orders.Include(x => x.OrderItems).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Order>> SearchOrders(OrderQueryObject query)
        {
            var orders = _dbContext.Orders.Include(x => x.Customer).AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.CustomerName))
            {
                orders = orders.Where(x => x.Customer!.FullName.ToLower().Contains(query.CustomerName.ToLower()));
            }

            if (!string.IsNullOrWhiteSpace(query.PhoneNumber))
            {
                orders = orders.Where(x => x.Customer!.PhoneNumber!.Contains(query.PhoneNumber));
            }

            if (!string.IsNullOrWhiteSpace(query.Address))
            {
                orders = orders.Where(x => x.Customer!.Address.ToLower().Contains(query.Address.ToLower()));
            }

            return await orders.ToListAsync();
        }

        public async Task<Order> Update(Order order)
        {
            _dbContext.Orders.Update(order);
            await _dbContext.SaveChangesAsync();
            return order;
        }
    }
}
