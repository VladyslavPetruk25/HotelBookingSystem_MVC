using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelBookingSystem.Models
{
    [Table(name: "Users")]
    public class User
    {
        [Key]
        public int UserId { get; set; }
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }
        [Required]
        [MaxLength(255)]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MaxLength(15)]
        [Phone]
        public string PhoneNumber { get; set; }
        [Required]
        public string Role { get; set; } = "User";
        public ICollection<Booking> Bookings { get; set; }
    }
}
