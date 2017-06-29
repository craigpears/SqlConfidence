using Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class Users
    {
        public static UserModel BuildUser(String email, String plainPassword, String firstName, String lastName)
        {
            UserModel user = new UserModel();
            user.Email = email;
            user.FirstName = firstName;
            user.LastName = lastName;
            user.HashedPassword = Security.HashPassword(plainPassword);

            return user;
        }

        public static UserModel BuildUser(LoginModel login)
        {
            return BuildUser(login.UserData.Email, login.PlainPassword, login.UserData.FirstName, login.UserData.LastName);
        }
    }
}
