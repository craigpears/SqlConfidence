using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class ExerciseQuestionAnswered
    {
        public int ExerciseQuestionAnsweredId { get; set; }
        public int UserId { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int ExerciseQuestionId { get; set; }
        public virtual ExerciseQuestion ExerciseQuestion { get; set; }
    }
}
