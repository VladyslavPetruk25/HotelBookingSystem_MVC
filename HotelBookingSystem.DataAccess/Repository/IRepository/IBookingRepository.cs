using HotelBookingSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBookingSystem.DataAccess.Repository.IRepository
{
    public interface IBookingRepository : IRepository<Booking>
    {
        void Update(Booking obj);
    }
}
