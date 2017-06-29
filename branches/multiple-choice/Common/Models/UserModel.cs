using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    public class UserModel
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string HashedPassword { get; set; }
        public Boolean IsAdmin { get; set; }
        public string Name { get { return String.Format("{0} {1}", FirstName, LastName); } }
        public Boolean IsGuest { get; set; }
    }
}
