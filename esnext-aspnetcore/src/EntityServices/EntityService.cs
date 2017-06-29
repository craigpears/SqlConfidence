
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityServices
{
    public class EntityService<T>:IEntityService<T>
    {
        protected IEntityRepository<T> _repository;

        public EntityService(IEntityRepository<T> repository)
        {
            _repository = repository;
        }

        public T Get(int id)
        {
            var entity = _repository.Get(id);
            return entity;
        }

        public List<T> GetAll()
        {
            var entities = _repository.GetAll().ToList();
            return entities;
        }
    }
}
