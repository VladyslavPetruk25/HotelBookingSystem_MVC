using HotelBookingSystem.DataAccess.Repository.IRepository;
using HotelBookingSystem.Models;
using HotelBookingSystem.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace HotelBookingSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.ROLE_ADMIN)]
    public class RoomTypeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public RoomTypeController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
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
        public IActionResult Create(RoomType obj, IFormFile? file)
        {
            var existingName = _unitOfWork.RoomType
                .Get(u => u.Name.ToLower() == obj.Name.ToLower());

            if (existingName != null)
            {
                ModelState.AddModelError("Name", "That type of room is already exists!");
            }

            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string roomTypePath = Path.Combine(wwwRootPath, @"images\room_type");

                    using (var fileStream = new FileStream(Path.Combine(roomTypePath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    obj.ImageUrl = @"\images\room_type\" + fileName;
                }

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
        public IActionResult Edit(RoomType obj, IFormFile? file)
        {
            var duplicateType = _unitOfWork.RoomType
                .Get(u => u.Name.ToLower() == obj.Name.ToLower() && u.RoomTypeId != obj.RoomTypeId);

            if (duplicateType != null)
            {
                ModelState.AddModelError("Name", "Another room type already has this name!");
            }

            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string roomTypePath = Path.Combine(wwwRootPath, @"images\room_type");

                    if (!string.IsNullOrEmpty(obj.ImageUrl))
                    {
                        var oldImagePath = Path.Combine(wwwRootPath, obj.ImageUrl.TrimStart('\\'));

                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using (var fileStream = new FileStream(Path.Combine(roomTypePath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    obj.ImageUrl = @"\images\room_type\" + fileName;
                }

                _unitOfWork.RoomType.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Room type is updated successfully!";

                return RedirectToAction("Index");
            }
            TempData["error"] = "Room type is not updated! Check inputs.";

            return View(obj);
        }

        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            var objRoomTypeList = _unitOfWork.RoomType.GetAll().ToList();

            return Json(new { data = objRoomTypeList });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var roomTypeToBeDeleted = _unitOfWork.RoomType.Get(u => u.RoomTypeId == id);
            if (roomTypeToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            if (!string.IsNullOrEmpty(roomTypeToBeDeleted.ImageUrl))
            {
                var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, roomTypeToBeDeleted.ImageUrl.TrimStart('\\'));

                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }
            }

            _unitOfWork.RoomType.Remove(roomTypeToBeDeleted);
            _unitOfWork.Save();

            return Json(new { success = true, message = "Delete Successful" });
        }

        #endregion
    }
}
