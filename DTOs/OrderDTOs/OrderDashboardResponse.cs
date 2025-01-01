namespace OnlineSouvenirShopAPI.DTOs.OrderDTOs
{
    public class OrderDashboardResponse
    {
        public int TotalOrders { get; set; }
        public int TotalOrdersProcessing { get; set; }
        public int TotalOrdersShipping { get; set; }
        public int TotalOrdersDelivered { get; set; }
        public int TotalOrdersCancelled { get; set; }
        public decimal TotalRevenue { get; set; }
        public List<RevenueByDayDTO> RevenueByDays { get; set; } = new List<RevenueByDayDTO>();
    }
}
