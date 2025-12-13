using System.Diagnostics;
using HotelBookingSystem.DataAccess.Repository.IRepository;
using HotelBookingSystem.Models;
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

        public IActionResult Index()
        {
            IEnumerable<RoomType> roomTypeList = _unitOfWork.RoomType.GetAll().ToList();

            return View(roomTypeList);
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
