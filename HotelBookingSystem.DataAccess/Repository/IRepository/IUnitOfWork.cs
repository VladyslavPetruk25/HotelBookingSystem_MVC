using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBookingSystem.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        IRoomRepository Room { get; }
        IBookingRepository Booking { get; }
        IRoomTypeRepository RoomType { get; }
        IUserRepository User { get; }
        void Save();
    }
}
