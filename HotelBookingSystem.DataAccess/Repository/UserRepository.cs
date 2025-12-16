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
    public class UserRepository : Repository<ApplicationUser>, IUserRepository
    {
        private ApplicationDbContext _db;
        public UserRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Update(ApplicationUser obj)
        {
            _db.Users.Update(obj);
        }
    }
}
