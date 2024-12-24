using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineSouvenirShopAPI.Models
{
    public class FavoriteProduct
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey("Customer")]
        public Guid CustomerId { get; set; }

        [ForeignKey("Product")]
        public Guid ProductId { get; set; }

        public AppUser? Customer { get; set; }
        public Product? Product { get; set; }
    }
}
