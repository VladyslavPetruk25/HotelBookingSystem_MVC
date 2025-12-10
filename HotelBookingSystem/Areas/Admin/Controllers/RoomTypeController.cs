using HotelBookingSystem.DataAccess.Repository.IRepository;
using HotelBookingSystem.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace HotelBookingSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RoomTypeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public RoomTypeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var objRoomTypeList = _unitOfWork.RoomType.GetAll().ToList();

            return View(objRoomTypeList);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(RoomType obj)
        {
            var existingName = _unitOfWork.RoomType
                .Get(u => u.Name.ToLower() == obj.Name.ToLower());

            if (existingName != null)
            {
                ModelState.AddModelError("Name", "That type of room is already exists!");
            }

            if (ModelState.IsValid)
            {
                _unitOfWork.RoomType.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Room type is created successfully!";

                return RedirectToAction("Index");
            }
            TempData["error"] = "Room type is not added. Check inputs!";

            return View(obj);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var roomTypeFromDb = _unitOfWork.RoomType.Get(u => u.RoomTypeId == id);

            if (roomTypeFromDb == null)
            {
                return NotFound();
            }

            return View(roomTypeFromDb);
        }

        [HttpPost]
        public IActionResult Edit(RoomType obj)
        {
            var duplicateType = _unitOfWork.RoomType
                .Get(u => u.Name.ToLower() == obj.Name.ToLower() && u.RoomTypeId != obj.RoomTypeId);

            if (duplicateType != null)
            {
                ModelState.AddModelError("Name", "Another room type already has this name!");
            }

            if (ModelState.IsValid)
            {
                _unitOfWork.RoomType.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Room type is updated successfully!";

                return RedirectToAction("Index");
            }
            TempData["error"] = "Room type is not updated! Check inputs.";

            return View(obj);
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var roomTypeFromDb = _unitOfWork.RoomType.Get(u => u.RoomTypeId == id);
            if (roomTypeFromDb == null)
            {
                return NotFound();
            }

            return View(roomTypeFromDb);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            var obj = _unitOfWork.RoomType.Get(u=>u.RoomTypeId == id);
            if (obj == null)
            {
                return NotFound();
            }
            _unitOfWork.RoomType.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Room type is deleted successfully!";

            return RedirectToAction("Index");
        }
    }
}
