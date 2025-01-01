using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineSouvenirShopAPI.Repositories.Implementations;
using OnlineSouvenirShopAPI.Repositories.Interfaces;

namespace OnlineSouvenirShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class DashboardController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly IOrderRepository _orderRepository;

        public DashboardController(IProductRepository productRepository, IOrderRepository orderRepository)
        {
            _productRepository = productRepository;
            _orderRepository = orderRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetDashboardData()
        {
            var productDashboard = await _productRepository.GetProductDashboard();
            var orderDashboard = await _orderRepository.GetOrderDashboard();

            var dashboardData = new
            {
                productDashboard,
                orderDashboard
            };
            return Ok(dashboardData);
        }
    }
}
