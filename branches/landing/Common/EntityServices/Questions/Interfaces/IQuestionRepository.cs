using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Questions.Interfaces
{
    public interface IQuestionRepository
    {
        List<ExerciseQuestion> GetAll();
        List<ExerciseQuestion> GetByExerciseId(int ExerciseId);
        ExerciseQuestion Get(int ExerciseQuestionId);
        void Save(ExerciseQuestion Question);
    }
}
