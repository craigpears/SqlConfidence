using DataAccess.Models;
using System.Collections.Generic;

namespace Common.EntityServices.Exercises.Base
{
    public interface IExerciseService: IEntityService<Exercise>
    {
        Exercise Get(int id, User user);
        Exercise GetByQuestion(int id, User user);
        List<Exercise> GetAll(User user);
        void MarkQuestionAsAnswered(int id, User user);
    }
}