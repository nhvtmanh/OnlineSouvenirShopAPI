using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineSouvenirShopAPI.Models
{
    public class Product
    {
        [Key]
        public Guid Id { get; set; }

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

        [ForeignKey("Category")]
        public Guid? CategoryId { get; set; }

        public Category? Category { get; set; }

        public ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}
