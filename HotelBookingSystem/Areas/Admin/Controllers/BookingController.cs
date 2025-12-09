using HotelBookingSystem.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace HotelBookingSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
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
    }
}
