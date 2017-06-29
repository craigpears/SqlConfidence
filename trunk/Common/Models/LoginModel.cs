using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    public class LoginModel
    {
        public UserModel UserData { get; set; }
        public String PlainPassword { get; set; }
    }
}
