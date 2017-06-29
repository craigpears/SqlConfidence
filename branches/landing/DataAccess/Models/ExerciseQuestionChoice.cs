using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class ExerciseQuestionChoice
    {
        public Guid ExerciseQuestionChoiceId { get; set; }
        public int ExerciseQuestionId { get; set; }
        public string Description { get; set; }
        public virtual ExerciseQuestion ExerciseQuestion { get; set; }
    }
}
