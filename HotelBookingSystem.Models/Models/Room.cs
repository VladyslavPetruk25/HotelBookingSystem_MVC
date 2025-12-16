using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace HotelBookingSystem.Models
{
    [Table(name: "Room")]
    public class Room
    {
        [Key]
        public int RoomId { get; set; }
        public int RoomTypeId { get; set; }
        [ForeignKey("RoomTypeId")]
        [ValidateNever]
        public virtual RoomType RoomType { get; set; }
        [Required]
        public int RoomNumber { get; set; }
        [Required]
        public int Flour { get; set; } 
    }
}
