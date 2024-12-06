using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OnlineSouvenirShopAPI.DTOs
{
    public class ProductDTO
    {
        [StringLength(100)]
        [Required]
        public string Name { get; set; } = string.Empty;

        [StringLength(255)]
        [Required]
        public string Description { get; set; } = string.Empty;

        [Required]
        public int StockQuantity { get; set; }

        public int SoldQuantity { get; set; }

        [Required]
        public decimal BasePrice { get; set; }

        [Required]
        public decimal DiscountPrice { get; set; }

        [StringLength(255)]
        public string? ImageUrl { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        public Guid? CategoryId { get; set; }
    }
}
