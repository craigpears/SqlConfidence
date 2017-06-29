using DataAccess.Models;
using System.Collections.Generic;

namespace Common.EntityServices.Exercises.MultipleChoice
{
    public interface IMultipleChoiceQuestionRepository
    {
        List<MultipleChoiceQuestion> GetByExercise(int id);
    }
}