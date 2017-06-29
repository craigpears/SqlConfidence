using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class MultipleChoiceDataQuery
    {
        public MultipleChoiceDataQuery()
        {
            this.Questions = new List<MultipleChoiceQuestion>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public int ExerciseId { get; set; }
        public String SqlQuery { get; set; }
        public virtual MultipleChoiceExercise Exercise { get; set; }
        public virtual ICollection<MultipleChoiceQuestion> Questions { get; set; }
    }
}
