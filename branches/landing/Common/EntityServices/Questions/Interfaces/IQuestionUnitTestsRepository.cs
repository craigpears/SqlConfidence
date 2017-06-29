using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Questions.Interfaces
{
    public interface IQuestionUnitTestsRepository
    {
        List<ExerciseQuestionUnitTest> GetAll();
        List<ExerciseQuestionUnitTest> GetByQuestionId(int Id);
        ExerciseQuestionUnitTest Get(Guid Id);
        void Save(ExerciseQuestionUnitTest Test);
    }
}
