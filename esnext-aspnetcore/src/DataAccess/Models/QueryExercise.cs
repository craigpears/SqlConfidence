using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class QueryExercise : Exercise
    {
        public QueryExercise()
        {
            this.ExerciseQuestions = new List<QueryQuestion>();
        }
        [DataMember]
        public virtual ICollection<QueryQuestion> ExerciseQuestions { get; set; }
    }
}
