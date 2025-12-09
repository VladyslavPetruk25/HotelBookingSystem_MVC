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
    public class BookingRepository : Repository<Booking>, IBookingRepository
    {
        private ApplicationDbContext _db;
        public BookingRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Update(Booking obj)
        {
            _db.Booking.Update(obj);
        }
    }
}
