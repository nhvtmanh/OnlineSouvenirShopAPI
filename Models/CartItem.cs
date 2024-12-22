using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineSouvenirShopAPI.Models
{
    public class CartItem
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey("Cart")]
        public Guid? CartId { get; set; }

        [ForeignKey("Product")]
        public Guid? ProductId { get; set; }

        [Required]
        public int Quantity { get; set; }

        public Cart? Cart { get; set; }

        public Product? Product { get; set; }
    }
}
