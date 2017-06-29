using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using EntityServices.EntityIncludes;

namespace EntityServices
{
    public class Repository<T> where T: class
    {
        SqlConfidenceContext _context;
        IEntityIncludes<T> _includes;

        public Repository(SqlConfidenceContext context, IEntityIncludes<T> includes)
        {
            _context = context;
            _includes = includes;
        }

        public virtual T Get(int id)
        {
            var obj = _context.Set<T>().Find(id);
            return obj;
        }

        public virtual void Add(T entity)
        {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();
        }

        public virtual IQueryable<T> GetAll()
        {
            var dbSet = _context.Set<T>();
            var queryable = AddIncludes(dbSet);
            return queryable;
        }

        public virtual IQueryable<T> AddIncludes(DbSet<T> dbSet)
        {
            if(_includes != null)
                return _includes.AddIncludes(dbSet);
            else
                return dbSet;
        }
    }
}
