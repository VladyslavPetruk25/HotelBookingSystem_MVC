using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBookingSystem.Models.Models.ViewModels
{
    public class RoomVM
    {
        public Room Room { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> RoomTypeList { get; set; }
    }
}
