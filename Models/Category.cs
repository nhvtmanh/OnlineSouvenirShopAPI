using System.ComponentModel.DataAnnotations;

namespace OnlineSouvenirShopAPI.Models
{
    public class Category
    {
        [Key]
        public Guid Id { get; set; }

        [StringLength(50)]
        [Required]
        public string Name { get; set; } = string.Empty;

        [StringLength(255)]
        [Required]
        public string Description { get; set; } = string.Empty;

        [StringLength(255)]
        public string? ImageUrl { get; set; }

        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
