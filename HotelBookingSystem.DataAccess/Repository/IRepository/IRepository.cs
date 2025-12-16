using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace HotelBookingSystem.DataAccess.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null ,string? includeProperties = null);
        T GetById(int id);
        T Get(Expression<Func<T, bool>> filter, string? includeProperties = null);
        void Add(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
    }
}
