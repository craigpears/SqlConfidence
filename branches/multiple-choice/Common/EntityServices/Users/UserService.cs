using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Models;

namespace Common.EntityServices.Users
{
    public class UserService : IEntityService<User>, IUserService
    {
        protected IUserRepository _repository { get; set; }
        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public User Get(int id)
        {
            return _repository.Get(id);
        }

        public User CreateGuestUser()
        {
            User guestUser = new User()
            {
                CreatedBy = "System",
                IsGuest = true
            };

            _repository.Add(guestUser);

            return guestUser;
        }

        public User GetByEmail(string email)
        {
            return _repository.GetByEmail(email);
        }

        public List<User> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
