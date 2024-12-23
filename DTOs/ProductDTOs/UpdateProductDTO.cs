using System.ComponentModel.DataAnnotations;

namespace OnlineSouvenirShopAPI.DTOs.ProductDTOs
{
    public class UpdateProductDTO
    {
        [StringLength(100)]
        [Required]
        public string Name { get; set; } = string.Empty;

        [StringLength(255)]
        [Required]
        public string Description { get; set; } = string.Empty;

        [Required]
        public int StockQuantity { get; set; }

        [Required]
        public decimal BasePrice { get; set; }

        [Required]
        public decimal DiscountPrice { get; set; }

        public IFormFile File { get; set; }

        public Guid? CategoryId { get; set; }
    }
}
