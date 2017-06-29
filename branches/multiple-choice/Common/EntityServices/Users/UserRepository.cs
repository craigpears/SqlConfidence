using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.EntityServices.Users
{
    public class UserRepository: EntityRepository<User>, IUserRepository
    {
        SqlConfidenceContext _context;

        public UserRepository()
        {
            _context = new SqlConfidenceContext();
        }

        public User GetByEmail(string email)
        {
            return _context.Users.SingleOrDefault(x => x.Email == email);
        }
    }
}
