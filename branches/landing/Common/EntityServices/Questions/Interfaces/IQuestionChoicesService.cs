using Common.Models;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Questions.Interfaces
{
    public interface IQuestionChoicesService
    {
        ExerciseQuestionChoice Get(Guid Id);
        List<ExerciseQuestionChoice> GetByQuestionId(int Id);
        void Save(ExerciseQuestionChoice Choice, UserModel user);
    }
}
