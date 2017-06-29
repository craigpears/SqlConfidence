using DataAccess.Models;
using System.Collections.Generic;

namespace Common.EntityServices.Exercises.MultipleChoice
{
    public interface IMultipleChoiceExerciseService:IEntityService<MultipleChoiceExercise>
    {
        MultipleChoiceExercise Get(int id, User user);
        List<MultipleChoiceExercise> GetAll(User user);
    }
}