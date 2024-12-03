using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineSouvenirShopAPI.Models
{
    public class OrderItem
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey("Order")]
        public Guid? OrderId { get; set; }

        [ForeignKey("Product")]
        public Guid? ProductId { get; set; }

        [Required]
        public int Quantity { get; set; }

        public Order? Order { get; set; }

        public Product? Product { get; set; }
    }
}
