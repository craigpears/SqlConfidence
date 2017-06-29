using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models
{
    public partial class UserAction
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int UserActionTypeId { get; set; }

        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public Nullable<DateTime> ResetDate { get; set; }

        [JsonIgnore]
        public virtual User User { get; set; }
        [JsonIgnore]
        public virtual ExerciseQuestion Question { get; set; }
        public virtual UserActionType UserActionType { get; set; }
    }
}
