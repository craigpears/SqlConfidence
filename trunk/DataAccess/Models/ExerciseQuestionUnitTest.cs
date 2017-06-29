using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class ExerciseQuestionUnitTest
    {
        public ExerciseQuestionUnitTest()
        {

        }

        public Guid ExerciseQuestionUnitTestId { get; set; }
        public int ExerciseQuestionId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string SqlToRun { get; set; }
        public string SqlToCompare { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<DateTime> UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public Boolean Deleted { get; set; }
        public Nullable<DateTime> DeletedDate { get; set; }
        public string DeletedBy { get; set; }
        public virtual ExerciseQuestion ExerciseQuestion { get; set; }
    }
}
