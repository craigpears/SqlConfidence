using Common.Models;
using Common.Questions.Interfaces;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Questions.Services
{
    public class QuestionUnitTestsService : IQuestionUnitTestsService
    {
        protected IQuestionUnitTestsRepository _unitTestsRepository { get; set; }

        public QuestionUnitTestsService(IQuestionUnitTestsRepository UnitTestsRepository)
        {
            _unitTestsRepository = UnitTestsRepository;
        }

        public ExerciseQuestionUnitTest Get(Guid Id)
        {
            var test = _unitTestsRepository.Get(Id);
            test = this.Create(test);
            return test;
        }

        public List<ExerciseQuestionUnitTest> GetByQuestionId(int Id)
        {
            var tests = _unitTestsRepository.GetByQuestionId(Id);
            tests = this.Create(tests);
            return tests;
        }

        public void Save(ExerciseQuestionUnitTest Test, UserModel User)
        {
            if (String.IsNullOrEmpty(Test.CreatedBy))
            {
                Test.CreatedBy = User.Email;
                Test.CreatedDate = DateTime.Now;
            }
            _unitTestsRepository.Save(Test);
        }

        protected List<ExerciseQuestionUnitTest> Create(List<ExerciseQuestionUnitTest> Tests)
        {
            foreach (var test in Tests)
            {
                this.Create(test);
            }
            return Tests;
        }

        protected ExerciseQuestionUnitTest Create(ExerciseQuestionUnitTest Test)
        {
            return Test;
        }
    }
}
