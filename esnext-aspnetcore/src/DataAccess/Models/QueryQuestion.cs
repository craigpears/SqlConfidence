using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class QueryQuestion:ExerciseQuestion
    {
        public int QueryExerciseId { get; set; }

        public string CorrectAnswerQuery { get; set; }

        public virtual QueryExercise Exercise { get; set; }


    }
}
