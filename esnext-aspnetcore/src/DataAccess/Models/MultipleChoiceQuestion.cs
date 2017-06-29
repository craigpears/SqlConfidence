using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    [DataContract]
    public class MultipleChoiceQuestion: ExerciseQuestion
    {
        public MultipleChoiceQuestion()
        {
            this.Options = new List<MultipleChoiceOption>();
            this.DataQueries = new List<MultipleChoiceDataQuery>();
        }

        public int MultipleChoiceExerciseId { get; set; }

        public virtual MultipleChoiceExercise Exercise { get; set; }
        [DataMember]
        public virtual MultipleChoiceOption CorrectOption { get; set; }
        [DataMember]
        public virtual ICollection<MultipleChoiceOption> Options { get; set; }
        [DataMember]
        public virtual ICollection<MultipleChoiceDataQuery> DataQueries { get; set; }
    }
}
