using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineSouvenirShopAPI.Models
{
    public class Payment
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public DateTime PaymentDate { get; set; }

        [Required]
        public decimal Total { get; set; }

        [Required]
        public bool PaymentMethod { get; set; }

        [Required]
        public byte Status { get; set; }
    }
}
