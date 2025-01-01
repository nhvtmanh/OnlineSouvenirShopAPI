using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineSouvenirShopAPI.Models
{
    public class Review
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey("Product")]
        public Guid? ProductId { get; set; }

        [ForeignKey("Customer")]
        public Guid? CustomerId { get; set; }

        [StringLength(255)]
        [Required]
        public string Comment { get; set; } = string.Empty;

        [Required]
        public DateTime ReviewDate { get; set; }

        public AppUser? Customer { get; set; }

        public Product? Product { get; set; }
    }
}
