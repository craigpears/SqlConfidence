using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntityServices.EntityIncludes
{
    public interface IEntityIncludes<T> where T:class
    {
        IQueryable<T> AddIncludes(DbSet<T> dbSet);
    }
}
