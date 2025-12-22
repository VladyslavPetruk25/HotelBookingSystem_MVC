using HotelBookingSystem.DataAccess.Repository.IRepository;
using HotelBookingSystem.Models;
using HotelBookingSystem.Utility;
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

        [HttpGet]
        public IActionResult BookingConfirmation(int bookingId)
        {
            Booking booking = _unitOfWork.Booking.Get(u => u.BookingId == bookingId, includeProperties: "Room,User");

            if (booking == null)
            {
                return NotFound();
            }

            if (booking.PaymentStatus != SD.STATUS_APPROVED)
            {
                booking.PaymentStatus = SD.PaymentStatusApproved;
                booking.Status = SD.STATUS_APPROVED;

                _unitOfWork.Booking.Update(booking);
                _unitOfWork.Save();
            }

            return View(bookingId);
        }
    }
}
