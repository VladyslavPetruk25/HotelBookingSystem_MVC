using HotelBookingSystem.DataAccess.Data;
using HotelBookingSystem.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using HotelBookingSystem.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using HotelBookingSystem.Utility;
using Microsoft.Identity.Client;

namespace HotelBookingSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RoomController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public RoomController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var objRoomList = _unitOfWork.Room.GetAll(includeProperties: "RoomType").ToList();

            return View(objRoomList);
        }

        [HttpGet]
        public IActionResult Create()
        {
            IEnumerable<SelectListItem> typeList = _unitOfWork.RoomType.GetAll().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.RoomTypeId.ToString()
            });
            ViewBag.RoomTypeList = typeList;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Room obj)
        {
            bool isRoomExists = _unitOfWork.Room.GetAll().Any(u => u.RoomNumber == obj.RoomNumber);
            if (isRoomExists)
            {
                ModelState.AddModelError("RoomNumber", "Room with same number is already exist!");
            }

            ModelState.Remove("Status");

            if (ModelState.IsValid)
            {
                obj.Status = SD.ROOM_AVAILABLE;

                _unitOfWork.Room.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Room created successfully";

                return RedirectToAction("Index");
            }
            else
            {
                TempData["error"] = "Error creating room! Please check inputs.";
            }

            IEnumerable<SelectListItem> typeList = _unitOfWork.RoomType.GetAll().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.RoomTypeId.ToString()
            });
            ViewBag.RoomTypeList = typeList; 

            return View(obj);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Room? roomFromDb = _unitOfWork.Room.Get(u => u.RoomId == id);

            if (roomFromDb == null)
            {
                return NotFound();
            }

            IEnumerable<SelectListItem> typeList = _unitOfWork.RoomType.GetAll().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.RoomTypeId.ToString()
            });

            ViewBag.RoomTypeList = typeList;

            return View(roomFromDb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Room obj)
        {
            var existingRoom = _unitOfWork.Room.Get(u => u.RoomNumber == obj.RoomNumber && u.RoomId != obj.RoomId);
            if (existingRoom != null)
            {
                ModelState.AddModelError("RoomNumber", "Another room already has the same number!");
            }

            ModelState.Remove("Status");

            if (ModelState.IsValid)
            {
                obj.Status = SD.ROOM_AVAILABLE;
                _unitOfWork.Room.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Room updated successfully";

                return RedirectToAction("Index");
            }
            else
            {
                TempData["error"] = "Error editing room! Please check inputs.";
            }

            IEnumerable<SelectListItem> typeList = _unitOfWork.RoomType.GetAll().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.RoomTypeId.ToString()
            });
            ViewBag.RoomTypeList = typeList;

            return View(obj);
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Room? roomFromDb = _unitOfWork.Room.Get(u => u.RoomId == id, includeProperties: "RoomType");

            if (roomFromDb == null)
            {
                return NotFound();
            }

            IEnumerable<SelectListItem> typeList = _unitOfWork.RoomType.GetAll().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.RoomTypeId.ToString()
            });

            ViewBag.RoomTypeList = typeList;

            return View(roomFromDb);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Room? obj = _unitOfWork.Room.Get(u => u.RoomId == id);
            if (obj == null)
            {
                TempData["error"] = "Error: Room not found!";
                return NotFound();
            }
            _unitOfWork.Room.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Room deleted successfully";

            return RedirectToAction("Index");
        }
    }
}
