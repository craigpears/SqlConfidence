using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class ExerciseQuestionUnitTest : EntityBase
    {
        public ExerciseQuestionUnitTest()
        {

        }

        [Key]
        public Guid ExerciseQuestionUnitTestId { get; set; }
        public int ExerciseQuestionId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string SqlToRun { get; set; }
        public string SqlToCompare { get; set; }
        public virtual ExerciseQuestion ExerciseQuestion { get; set; }
    }
}
