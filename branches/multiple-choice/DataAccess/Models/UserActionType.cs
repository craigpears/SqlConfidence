using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models
{
    public partial class UserActionType
    {
        public UserActionType()
        {
            this.UserActions = new List<UserAction>();
        }

        [Key]
        public int Id { get; set; }
        public string Description { get; set; }
        public virtual ICollection<UserAction> UserActions { get; set; }
    }
}
