using Common.Questions.Interfaces;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Questions.Repositories
{
    public class QuestionUnitTestsRepository : IQuestionUnitTestsRepository
    {
        public List<ExerciseQuestionUnitTest> GetAll()
        {
            return new SqlConfidenceContext().ExerciseQuestionUnitTests.ToList();
        }

        public List<ExerciseQuestionUnitTest> GetByQuestionId(int Id)
        {
            return new SqlConfidenceContext().ExerciseQuestionUnitTests.Where(x => x.ExerciseQuestionId == Id).ToList();
        }

        public ExerciseQuestionUnitTest Get(Guid Id)
        {
            return new SqlConfidenceContext().ExerciseQuestionUnitTests.Find(Id);
        }

        public void Save(ExerciseQuestionUnitTest Test)
        {
            // Does it already exist?
            var exists = Test.ExerciseQuestionUnitTestId != Guid.Empty;
            var context = new SqlConfidenceContext();

            // Stop it saving any related objects
            Test.ExerciseQuestion = null;

            if(!exists)
            {
                Test.ExerciseQuestionUnitTestId = Guid.NewGuid();
                context.ExerciseQuestionUnitTests.Add(Test);
            }
            else
            {
                context.ExerciseQuestionUnitTests.Attach(Test);
                context.Entry(Test).State = EntityState.Modified;
            }

            context.SaveChanges();
        }
    }
}
