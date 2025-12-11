using HotelBookingSystem.DataAccess.Data;
using HotelBookingSystem.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using HotelBookingSystem.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using HotelBookingSystem.Utility;
using Microsoft.Identity.Client;
using HotelBookingSystem.Models.Models.ViewModels;

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
            var objRoomList = _unitOfWork.Room
                .GetAll(includeProperties: "RoomType").ToList();

            return View(objRoomList);
        }

        [HttpGet]
        public IActionResult Create()
        {
            RoomVM roomVM = new()
            {
                RoomTypeList = _unitOfWork.RoomType
                .GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.RoomTypeId.ToString()
                }),
                Room = new Room()
            };

            return View(roomVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(RoomVM roomVM)
        {
            var existingRoom = _unitOfWork.Room
                .Get(u => u.RoomNumber == roomVM.Room.RoomNumber && u.RoomId != roomVM.Room.RoomId);
            if (existingRoom != null)
            {
                ModelState.AddModelError("Room.RoomNumber", "Room with same number is already exist!");
            }

            if (ModelState.IsValid)
            {
                roomVM.Room.Status = SD.ROOM_AVAILABLE;

                _unitOfWork.Room.Add(roomVM.Room);
                _unitOfWork.Save();
                TempData["success"] = "Room created successfully";

                return RedirectToAction("Index");
            }
            TempData["error"] = "Error creating room! Please check inputs.";

            roomVM.RoomTypeList = _unitOfWork.RoomType
                .GetAll().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.RoomTypeId.ToString()
            });

            return View(roomVM);
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

            RoomVM roomVM = new RoomVM()
            {
                Room = roomFromDb,
                RoomTypeList = _unitOfWork.RoomType.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.RoomTypeId.ToString()
                })
            };

            return View(roomVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(RoomVM roomVM)
        {
            var existingRoom = _unitOfWork.Room
                .Get(u => u.RoomNumber == roomVM.Room.RoomNumber && u.RoomId != roomVM.Room.RoomId);
            if (existingRoom != null)
            {
                ModelState.AddModelError("Room.RoomNumber", "Another room already has the same number!");
            }

            if (ModelState.IsValid)
            {
                roomVM.Room.Status = SD.ROOM_AVAILABLE;
                _unitOfWork.Room.Update(roomVM.Room);
                _unitOfWork.Save();
                TempData["success"] = "Room updated successfully";

                return RedirectToAction("Index");
            }
            TempData["error"] = "Error editing room! Please check inputs.";

            roomVM.RoomTypeList = _unitOfWork.RoomType.GetAll().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.RoomTypeId.ToString()
            });

            return View(roomVM);
        }

        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            var objRoomList = _unitOfWork.Room.GetAll(includeProperties: "RoomType").ToList();

            return Json(new { data = objRoomList });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var roomToBeDeleted = _unitOfWork.Room.Get(u => u.RoomId == id);
            if (roomToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            _unitOfWork.Room.Remove(roomToBeDeleted);
            _unitOfWork.Save();

            return Json(new { success = true, message = "Delete Successful" });
        }

        #endregion
    }
}
