using DataAccess.Models;

namespace Common.EntityServices.Users
{
    public interface IUserRepository
    {
        User Get(int id);
        User GetByEmail(string email);
        void Add(User entity);
    }
}