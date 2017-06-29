using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Questions.Interfaces
{
    public interface IQuestionAnsweredRepository
    {
        List<ExerciseQuestionAnswered> GetAll();
        List<ExerciseQuestionAnswered> GetByQuestionId(int Id, int UserId);
        ExerciseQuestionAnswered Get(int Id);
        void Save(ExerciseQuestionAnswered QuestionAnswered);
    }
}
