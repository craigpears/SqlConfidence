using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class MultipleChoiceExercise:Exercise
    {
        public MultipleChoiceExercise()
        {
            this.DataQueries = new List<MultipleChoiceDataQuery>();
        }

        public virtual ICollection<MultipleChoiceDataQuery> DataQueries { get; set; }
        [NotMapped]
        public List<MultipleChoiceQuestion> Questions { get; set; }
    }
}
