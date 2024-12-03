using System.ComponentModel.DataAnnotations;

namespace OnlineSouvenirShopAPI.Models
{
    public class Voucher
    {
        [Key]
        [StringLength(50)]
        public string Code { get; set; } = string.Empty;

        [Required]
        public int DiscountAmount { get; set; }

        [Required]
        public DateTime ExpirationDate { get; set; }

        [Required]
        public byte Status { get; set; }

        [Required]
        public int MaxUsageCount { get; set; }
    }
}
