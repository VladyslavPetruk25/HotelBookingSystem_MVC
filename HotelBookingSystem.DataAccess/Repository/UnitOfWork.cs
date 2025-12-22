using HotelBookingSystem.DataAccess.Data;
using HotelBookingSystem.DataAccess.Repository.IRepository;

namespace HotelBookingSystem.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _db;
        public IRoomRepository Room { get; private set; }
        public IBookingRepository Booking { get; private set; }
        public IRoomTypeRepository RoomType { get; private set; }
        public IUserRepository User { get; private set; }

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Room = new RoomRepository(_db);
            RoomType = new RoomTypeRepository(_db);
            Booking = new BookingRepository(_db);
            User = new UserRepository(_db);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
