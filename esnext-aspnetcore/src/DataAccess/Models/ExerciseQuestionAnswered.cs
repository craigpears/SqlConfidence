using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models
{
    public partial class ExerciseQuestionAnswered
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ExerciseQuestionId { get; set; }

        public DateTime CreatedDate { get; set; }

        [JsonIgnore]
        public virtual User User { get; set; }
        [JsonIgnore]
        public virtual ExerciseQuestion ExerciseQuestion { get; set; }
    }
}
