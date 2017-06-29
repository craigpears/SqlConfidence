using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Models;

namespace Common.EntityServices
{
    public abstract class EntityRepository<T>: Repository<T>, IEntityRepository<T> where T:EntityBase
    {
        SqlConfidenceContext _context;

        public EntityRepository()
        {
            _context = new SqlConfidenceContext();
        }

        public override T Get(int id)
        {
            var obj = base.Get(id);
            if (obj.Deleted || obj == null)
            {
                throw new ArgumentException("Item could not be found with id " + id);
            }
            return obj;
        }

        public override void Add(T entity)
        {
            entity.CreatedDate = DateTime.Now;
            base.Add(entity);
        }

        public override IQueryable<T> GetAll()
        {
            var query = base.GetAll();
            query = query.Where(x => !x.Deleted);
            return query;
        }
    }
}
