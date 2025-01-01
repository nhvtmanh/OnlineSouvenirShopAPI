using OnlineSouvenirShopAPI.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OnlineSouvenirShopAPI.DTOs
{
    public class CommentDTO
    {
        public Guid ProductId { get; set; }

        [StringLength(255)]
        [Required]
        public string Comment { get; set; } = string.Empty;
    }
}
