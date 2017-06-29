using DataAccess.Models;

namespace Common.EntityServices.Users
{
    public interface IUserService
    {
        User Get(int id);
        User GetByEmail(string email);
        User CreateGuestUser();
    }
}