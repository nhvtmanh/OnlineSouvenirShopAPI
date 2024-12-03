using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineSouvenirShopAPI.Models
{
    public class Order
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        [Required]
        public byte Status { get; set; }

        [Required]
        public decimal Total { get; set; }

        [ForeignKey("Customer")]
        public Guid? CustomerId { get; set; }

        [ForeignKey("Payment")]
        public Guid? PaymentId { get; set; }

        [StringLength(50)]
        [ForeignKey("Voucher")]
        public string? VoucherCode { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

        public Customer? Customer { get; set; }

        public Payment? Payment { get; set; }

        public Voucher? Voucher { get; set; }
    }
}
