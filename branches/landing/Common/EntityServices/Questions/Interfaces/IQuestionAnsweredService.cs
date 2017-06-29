using Common.Models;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Questions.Interfaces
{
    public interface IQuestionAnsweredService
    {
        ExerciseQuestionAnswered Get(int Id);
        List<ExerciseQuestionAnswered> GetByQuestionId(int Id, int UserId);
        void Save(ExerciseQuestionAnswered QuestionAnswered, UserModel user);
    }
}
