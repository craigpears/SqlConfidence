using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class MultipleChoiceExercise:Exercise
    {
        public MultipleChoiceExercise()
        {
            this.ExerciseQuestions = new List<MultipleChoiceQuestion>();
        }

        [DataMember]
        public virtual ICollection<MultipleChoiceQuestion> ExerciseQuestions { get; set; }
    }
}
