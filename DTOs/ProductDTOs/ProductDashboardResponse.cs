namespace OnlineSouvenirShopAPI.DTOs.ProductDTOs
{
    public class ProductDashboardResponse
    {
        public int TotalProducts { get; set; }
        public List<TopSellingProductDTO> TopSellingProducts { get; set; } = new List<TopSellingProductDTO>();
    }
}
