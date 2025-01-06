using System.ComponentModel.DataAnnotations;

namespace OnlineSouvenirShopAPI.DTOs
{
    public class PurchaseDTO
    {
        public List<Guid> CartItemIds { get; set; } = new List<Guid>();

        [StringLength(50)]
        public string VoucherCode { get; set; } = string.Empty;
    }
}
