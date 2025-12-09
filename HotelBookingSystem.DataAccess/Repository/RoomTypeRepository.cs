using HotelBookingSystem.DataAccess.Data;
using HotelBookingSystem.DataAccess.Repository.IRepository;
using HotelBookingSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBookingSystem.DataAccess.Repository
{
    public class RoomTypeRepository : Repository<RoomType>, IRoomTypeRepository
    {
        private ApplicationDbContext _db;
        public RoomTypeRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Update(RoomType obj)
        {
            _db.RoomType.Update(obj);
        }
    }
}
