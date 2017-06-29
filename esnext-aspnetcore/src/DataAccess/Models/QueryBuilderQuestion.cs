using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class QueryBuilderQuestion:ExerciseQuestion
    {
        public int QueryBuilderExerciseId { get; set; }

        public virtual QueryBuilderExercise Exercise { get; set; }
    }
}
