using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineSouvenirShopAPI.Models
{
    public class Cart
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey("Customer")]
        public Guid? CustomerId { get; set; }

        public AppUser? Customer { get; set; }

        public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
    }
}
