using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class UnitTestedQuestion:ExerciseQuestion
    {
        public int UnitTestedExerciseId { get; set; }

        public virtual UnitTestedExercise Exercise { get; set; }
    }
}
