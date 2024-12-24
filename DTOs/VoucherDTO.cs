using System.ComponentModel.DataAnnotations;

namespace OnlineSouvenirShopAPI.DTOs
{
    public class VoucherDTO
    {
        [StringLength(50)]
        [Required]
        public string Code { get; set; } = string.Empty;

        [Required]
        public int DiscountAmount { get; set; }

        [Required]
        public DateTime ExpirationDate { get; set; }

        [Required]
        public int MaxUsageCount { get; set; }
    }
}
