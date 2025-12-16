using HotelBookingSystem.DataAccess.Repository.IRepository;
using HotelBookingSystem.Models;
using HotelBookingSystem.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HotelBookingSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
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
            return View();
        }

        [HttpGet]
        public IActionResult Details(int bookingId)
        {
            var booking = _unitOfWork.Booking.Get(u=>u.BookingId == bookingId, includeProperties: "User,Room");

            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }

        [HttpPost]
        [Authorize(Roles = SD.ROLE_ADMIN)]
        public IActionResult ApproveBooking(int bookingId)
        {
            var booking = _unitOfWork.Booking.Get(u => u.BookingId == bookingId);
            if (booking != null)
            {
                booking.Status = SD.STATUS_APPROVED;
                _unitOfWork.Booking.Update(booking);
                _unitOfWork.Save();
                TempData["success"] = "Booking Approved!";
            }
            return RedirectToAction(nameof(Details), new { bookingId = bookingId });
        }

        [HttpPost]
        [Authorize(Roles = SD.ROLE_ADMIN)]
        public IActionResult CancelBooking(int bookingId)
        {
            var booking = _unitOfWork.Booking.Get(u => u.BookingId == bookingId);
            if (booking != null)
            {
                booking.Status = SD.STATUS_CANCELLED;
                _unitOfWork.Booking.Update(booking);
                _unitOfWork.Save();
                TempData["success"] = "Booking Cancelled!";
            }
            return RedirectToAction(nameof(Details), new { bookingId = bookingId });
        }

        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            IEnumerable<Booking> objBookingList;

            if (User.IsInRole(SD.ROLE_ADMIN))
            {
                objBookingList = _unitOfWork.Booking.GetAll(includeProperties: "User,Room");
            }
            else
            {
                var claimsIdentity = (ClaimsIdentity)User.Identity!;
                var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)!.Value;

                objBookingList = _unitOfWork.Booking.GetAll(u => u.UserId == userId, "User,Room");
            }

            return Json(new { data = objBookingList });
        }

        #endregion
    }
}
