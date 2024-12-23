namespace OnlineSouvenirShopAPI.DTOs
{
    public class PurchaseDTO
    {
        public List<Guid> CartItemIds { get; set; } = new List<Guid>();
    }
}
