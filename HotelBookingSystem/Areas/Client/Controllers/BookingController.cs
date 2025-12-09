using Microsoft.AspNetCore.Mvc;

namespace HotelBookingSystem.Areas.Client.Controllers
{
    [Area("Client")]
    public class BookingController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
