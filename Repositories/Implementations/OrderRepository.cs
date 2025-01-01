using Microsoft.EntityFrameworkCore;
using OnlineSouvenirShopAPI.Data;
using OnlineSouvenirShopAPI.DTOs.OrderDTOs;
using OnlineSouvenirShopAPI.Helpers.Enums;
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

        public async Task<OrderDashboardResponse> GetOrderDashboard()
        {
            var totalOrders = await _dbContext.Orders.CountAsync();
            var totalOrdersProcessing = await _dbContext.Orders.CountAsync(x => x.Status == (byte)OrderStatus.Processing);
            var totalOrdersShipping = await _dbContext.Orders.CountAsync(x => x.Status == (byte)OrderStatus.Shipping);
            var totalOrdersDelivered = await _dbContext.Orders.CountAsync(x => x.Status == (byte)OrderStatus.Delivered);
            var totalOrdersCancelled = await _dbContext.Orders.CountAsync(x => x.Status == (byte)OrderStatus.Cancelled);
            var totalRevenue = await _dbContext.Orders.Where(x => x.Status == (byte)OrderStatus.Delivered).SumAsync(x => x.Total);
            var revenueByDays = await _dbContext.Orders
                .Where(x => x.Status == (byte)OrderStatus.Delivered)
                .GroupBy(x => x.OrderDate.Date)
                .Select(x => new RevenueByDayDTO
                {
                    Date = DateOnly.FromDateTime(x.Key),
                    Revenue = x.Sum(x => x.Total)
                })
                .OrderBy(x => x.Date)
                .ToListAsync();

            var orderDashboard = new OrderDashboardResponse
            {
                TotalOrders = totalOrders,
                TotalOrdersProcessing = totalOrdersProcessing,
                TotalOrdersShipping = totalOrdersShipping,
                TotalOrdersDelivered = totalOrdersDelivered,
                TotalOrdersCancelled = totalOrdersCancelled,
                TotalRevenue = totalRevenue,
                RevenueByDays = revenueByDays
            };
            return orderDashboard;
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
