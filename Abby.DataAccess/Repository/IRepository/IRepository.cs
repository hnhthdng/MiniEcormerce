using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Abby.DataAccess.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        T Get(int id);
        IEnumerable<T> GetAll(string? includeProperty=null);
        void Add(T entity);
        void AddRange(IEnumerable<T> entities);
        void Remove(T entity);
        void Remove(int id);
        void RemoveRange(IEnumerable<T> entities);
        T GetFirstOrDefault(Expression<Func<T, bool>>? filter = null);
    }
}
