using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class QueryBuilderExercise: Exercise
    {
        public QueryBuilderExercise()
        {
            this.ExerciseQuestions = new List<QueryBuilderQuestion>();
        }

        public virtual ICollection<QueryBuilderQuestion> ExerciseQuestions { get; set; }
    }
}
