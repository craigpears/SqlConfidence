using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class UserAction
    {
        public int USA_ID { get; set; }
        public Nullable<int> EXQ_ID { get; set; }
        public int US_ID { get; set; }
        public string USA_DESCRIPTION { get; set; }
        public int USAT_ID { get; set; }
        public System.DateTime USA_CREATED_DATE { get; set; }
        public Nullable<System.DateTime> USA_RESET_DATE { get; set; }
        public virtual UserActionType USERS_ACTIONS_TYPES { get; set; }
    }
}
