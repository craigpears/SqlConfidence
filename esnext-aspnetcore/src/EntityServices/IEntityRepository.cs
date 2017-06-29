using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityServices
{
    public interface IEntityRepository<T>
    {
        T Get(int id);
        void Add(T entity);
        IQueryable<T> GetAll();
    }
}
