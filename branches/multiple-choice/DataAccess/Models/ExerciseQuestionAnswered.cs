using System;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models
{
    public partial class ExerciseQuestionAnswered
    {
        [Key]
        public int ExerciseQuestionAnsweredId { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ExerciseQuestionId { get; set; }
        public virtual ExerciseQuestion ExerciseQuestion { get; set; }
    }
}
