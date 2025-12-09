using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelBookingSystem.Models
{
    [Table(name: "RoomType")]
    public class RoomType
    {
        [Key]
        public int RoomTypeId { get; set; }
        [Required]
        [MaxLength(15)]
        public string Name { get; set; }
        [Required]
        [MaxLength(350)]
        public string Description { get; set; }
        public int Capacity { get; set; }
        [Required]
        public decimal PricePerNight { get; set; }
        [ValidateNever]
        public ICollection<Room> Room { get; set; }
    }
}
