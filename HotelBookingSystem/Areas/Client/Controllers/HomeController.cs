using System.Diagnostics;
using System.Security.Claims;
using HotelBookingSystem.DataAccess.Repository.IRepository;
using HotelBookingSystem.Models;
using HotelBookingSystem.Models.Models.ViewModels;
using HotelBookingSystem.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelBookingSystem.Areas.Client.Controllers
{
    [Area("Client")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index(DateTime? checkInDate, DateTime? checkOutDate)
        {
            IEnumerable<Room> roomList = _unitOfWork.Room.GetAll(includeProperties: "RoomType");

            if (checkInDate != null && checkOutDate != null)
            {
                var bookedRoomIds = _unitOfWork.Booking
                    .GetAll(u => u.Status != SD.STATUS_CANCELLED && (u.CheckInDate < checkOutDate && checkInDate < u.CheckOutDate))
                    .Select(u => u.RoomId).ToList();

                roomList = roomList.Where(u => !bookedRoomIds.Contains(u.RoomId)).ToList();
            }

            HomeVM homeVM = new HomeVM
            {
                RoomList = roomList,
                CheckInDate = checkInDate,
                CheckOutDate = checkOutDate
            };

            return View(homeVM);
        }

        public IActionResult Details(int id)
        {
            RoomType roomType = _unitOfWork.RoomType.Get(u=>u.RoomTypeId == id);

            return View(roomType);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult Summary(int roomTypeId, DateTime checkInDate, DateTime checkOutDate)
        {
            var bookedRoomIds = _unitOfWork.Booking
                .GetAll(u => u.Status != SD.STATUS_CANCELLED && u.Room.RoomTypeId == roomTypeId &&
                (u.CheckInDate < checkOutDate && checkInDate < u.CheckOutDate)).Select(u => u.RoomId).ToList();

            var availableRoom = _unitOfWork.Room
                .GetAll(u => u.RoomTypeId == roomTypeId & !bookedRoomIds
                .Contains(u.RoomId), includeProperties: "RoomType").FirstOrDefault();

            if (availableRoom == null)
            {
                TempData["error"] = "Sorry, no rooms of this type are available for selected dates.";
                return RedirectToAction(nameof(Details), new { id = roomTypeId });
            }

            if (checkOutDate <= checkInDate)
            {
                TempData["error"] = "Check-out date must be after check-in date.";

                return RedirectToAction(nameof(Details), new { id = roomTypeId });
            }

            var claimsIdentity = (ClaimsIdentity)User.Identity!;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            var applicationUser = _unitOfWork.User.Get(u => u.Id == userId);

            Booking booking = new Booking
            {
                RoomId = availableRoom.RoomId,
                Room = availableRoom,
                CheckInDate = checkInDate,
                CheckOutDate = checkOutDate,
                UserId = userId,
                User = applicationUser,
                TotalCost = availableRoom.RoomType.PricePerNight * (checkOutDate - checkInDate).Days,
                Name = applicationUser.FirstName + " " + applicationUser.LastName,
                Email = applicationUser.Email,
                Phone = applicationUser.PhoneNumber
            };

            return View(booking);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public IActionResult CreateBooking(Booking booking)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity!;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            booking.UserId = userId;

            ModelState.Remove("Room");
            ModelState.Remove("User");
            ModelState.Remove("Status");

            if (ModelState.IsValid)
            {
                booking.Status = SD.STATUS_PENDING;
                _unitOfWork.Booking.Add(booking);
                _unitOfWork.Save();

                return RedirectToAction(nameof(BookingConfirmation), new { id = booking.BookingId });
            }

            var room = _unitOfWork.Room.Get(u => u.RoomId == booking.RoomId, includeProperties: "RoomType");
            booking.Room = room;

            return View("Summary", booking);
        }

        public IActionResult BookingConfirmation(int id)
        {
            return View(id);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
