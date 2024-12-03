using System.ComponentModel.DataAnnotations;

namespace OnlineSouvenirShopAPI.Models
{
    public class Customer
    {
        [Key]
        public Guid Id { get; set; }

        [StringLength(50)]
        [EmailAddress]
        [Required]
        public string Email { get; set; } = string.Empty;

        [StringLength(255)]
        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        [StringLength(50)]
        [Required]
        public string FullName { get; set; } = string.Empty;

        [StringLength(10)]
        [Phone]
        [Required]
        public string PhoneNumber { get; set; } = string.Empty;

        [StringLength(255)]
        [Required]
        public string Address { get; set; } = string.Empty;

        [StringLength(255)]
        public string? AvatarUrl { get; set; }

        [Required]
        public bool Gender { get; set; }

        public ICollection<Order> Orders { get; set; } = new List<Order>();

        public ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}
