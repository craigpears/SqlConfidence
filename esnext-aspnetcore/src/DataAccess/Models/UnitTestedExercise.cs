using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class UnitTestedExercise: Exercise
    {
        public UnitTestedExercise()
        {
            this.ExerciseQuestions = new List<UnitTestedQuestion>();
        }

        public virtual ICollection<UnitTestedQuestion> ExerciseQuestions { get; set; }
    }
}
