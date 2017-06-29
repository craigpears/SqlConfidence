using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.EntityServices
{
    public interface IEntityService<T>
    {
        T Get(int id);
        List<T> GetAll();
    }
}
