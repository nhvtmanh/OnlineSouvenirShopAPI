using System.ComponentModel.DataAnnotations;

namespace OnlineSouvenirShopAPI.DTOs
{
    public class CategoryDTO
    {
        [StringLength(50)]
        [Required]
        public string Name { get; set; } = string.Empty;

        [StringLength(255)]
        [Required]
        public string Description { get; set; } = string.Empty;

        [StringLength(255)]
        public string? ImageUrl { get; set; }
    }
}
