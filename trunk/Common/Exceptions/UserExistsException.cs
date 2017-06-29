using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Exceptions
{
    public class UserExistsException:Exception
    {
        public String UserEmail { get; set; }

        public UserExistsException()
        {
        }

        public UserExistsException(string message, string userEmail)
            : base(message)
        {
            UserEmail = userEmail;
        }

        public UserExistsException(string message, string userEmail, Exception inner)
            : base(message, inner)
        {
            UserEmail = userEmail;
        }
    }
}
