using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Models;
using System.Data.Entity.Infrastructure;

namespace Common.EntityServices
{
    public abstract class Repository<T> where T: class
    {
        SqlConfidenceContext _context;

        public Repository()
        {
            _context = new SqlConfidenceContext();
            Includes = new List<String>();
        }

        public List<String> Includes { get; set; }

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
            DbQuery<T> dbSet = _context.Set<T>();
            if(Includes.Any())
            {
                foreach(var include in Includes)
                {
                    dbSet = dbSet.Include(include);
                }
            }
            return dbSet;
        }
    }
}
