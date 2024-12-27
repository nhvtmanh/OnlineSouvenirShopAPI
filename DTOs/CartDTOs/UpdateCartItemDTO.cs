using OnlineSouvenirShopAPI.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OnlineSouvenirShopAPI.DTOs.CartDTOs
{
    public class UpdateCartItemDTO
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public int Quantity { get; set; }
    }
}
