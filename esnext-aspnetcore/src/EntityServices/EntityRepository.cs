using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Models;
using EntityServices.EntityIncludes;

namespace EntityServices
{
    public class EntityRepository<T>: Repository<T>, IEntityRepository<T> where T:EntityBase
    {
        public EntityRepository(SqlConfidenceContext context, IEntityIncludes<T> includes) : base(context, includes)
        {
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
