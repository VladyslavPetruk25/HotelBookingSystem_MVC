using HotelBookingSystem.DataAccess.Repository.IRepository;
using HotelBookingSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HotelBookingSystem.Areas.Client.Controllers
{
    [Area("Client")]
    [Authorize]
    public class BookingController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public BookingController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity!;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            
            IEnumerable<Booking> bookingList = _unitOfWork.Booking.GetAll(u=>u.UserId == userId, includeProperties: "Room,Room.RoomType");
            
            return View(bookingList);
        }


    }
}
