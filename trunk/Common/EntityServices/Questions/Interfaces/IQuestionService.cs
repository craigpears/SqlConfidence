using Common.Models;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Questions.Interfaces
{
    public interface IQuestionService
    {
        ExerciseQuestion Get(int Id, UserModel User);
        List<ExerciseQuestion> GetByExerciseId(int Id, UserModel User);
        void Save(ExerciseQuestion Question, UserModel user);
    }
}
