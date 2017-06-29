using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class MultipleChoiceQuestion: ExerciseQuestion
    {
        public MultipleChoiceQuestion()
        {
            this.Options = new List<MultipleChoiceOption>();
            this.DataQueries = new List<MultipleChoiceDataQuery>();
        }

        public MultipleChoiceOption CorrectOption { get; set; }
        public virtual ICollection<MultipleChoiceOption> Options { get; set; }
        public virtual ICollection<MultipleChoiceDataQuery> DataQueries { get; set; }
    }
}
