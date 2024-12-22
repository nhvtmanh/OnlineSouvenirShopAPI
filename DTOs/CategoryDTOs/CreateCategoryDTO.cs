using System.ComponentModel.DataAnnotations;

namespace OnlineSouvenirShopAPI.DTOs.CategoryDTOs
{
    public class CreateCategoryDTO
    {
        [StringLength(50)]
        [Required]
        public string Name { get; set; } = string.Empty;

        [StringLength(255)]
        [Required]
        public string Description { get; set; } = string.Empty;

        public IFormFile File { get; set; }
    }
}
