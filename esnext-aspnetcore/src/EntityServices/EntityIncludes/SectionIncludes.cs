using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntityServices.EntityIncludes
{
    public class SectionIncludes : IEntityIncludes<Section>
    {
        public IQueryable<Section> AddIncludes(DbSet<Section> dbSet)
        {
            return dbSet;
        }
    }
}
