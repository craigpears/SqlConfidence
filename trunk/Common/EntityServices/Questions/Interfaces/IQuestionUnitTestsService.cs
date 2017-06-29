using Common.Models;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Questions.Interfaces
{
    public interface IQuestionUnitTestsService
    {
        ExerciseQuestionUnitTest Get(Guid Id);
        List<ExerciseQuestionUnitTest> GetByQuestionId(int Id);
        void Save(ExerciseQuestionUnitTest Choice, UserModel user);
    }
}
