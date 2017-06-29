using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Questions.Interfaces
{
    public interface IQuestionChoicesRepository
    {
        List<ExerciseQuestionChoice> GetAll();
        List<ExerciseQuestionChoice> GetByQuestionId(int Id);
        ExerciseQuestionChoice Get(Guid Id);
        void Save(ExerciseQuestionChoice Choice);
    }
}
