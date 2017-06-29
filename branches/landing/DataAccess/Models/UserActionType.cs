using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class UserActionType
    {
        public UserActionType()
        {
            this.USERS_ACTIONS = new List<UserAction>();
        }

        public int USAT_ID { get; set; }
        public string DESCRIPTION { get; set; }
        public virtual ICollection<UserAction> USERS_ACTIONS { get; set; }
    }
}
