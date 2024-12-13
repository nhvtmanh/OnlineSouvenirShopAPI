using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace OnlineSouvenirShopAPI.Models
{
    public class AppUser : IdentityUser<Guid>
    {
        [StringLength(50)]
        [Required]
        public string FullName { get; set; } = string.Empty;

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
