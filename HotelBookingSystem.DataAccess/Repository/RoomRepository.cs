using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelBookingSystem.Models;
using HotelBookingSystem.DataAccess.Repository.IRepository;
using HotelBookingSystem.DataAccess.Data;

namespace HotelBookingSystem.DataAccess.Repository
{
    public class RoomRepository : Repository<Room>, IRoomRepository
    {
        private ApplicationDbContext _db;
        public RoomRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Update(Room obj)
        {
            _db.Room.Update(obj);
        }
    }
}
