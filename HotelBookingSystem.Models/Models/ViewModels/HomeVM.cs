using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBookingSystem.Models.Models.ViewModels
{
    public class HomeVM
    {
        public IEnumerable<Room> RoomList { get; set; }

        public DateTime? CheckInDate { get; set; }
        public DateTime? CheckOutDate { get; set; }
    }
}
