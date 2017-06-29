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
    public class QuestionRepository : IQuestionRepository
    {
        public List<ExerciseQuestion> GetAll()
        {
            return new SqlConfidenceContext().ExerciseQuestions.Where(x => !x.Deleted).ToList();
        }

        public List<ExerciseQuestion> GetByExerciseId(int ExerciseId)
        {
            return new SqlConfidenceContext().ExerciseQuestions.Where(x => x.ExerciseId == ExerciseId && !x.Deleted).OrderBy(x => x.Order).ToList();
        }

        public ExerciseQuestion Get(int ExerciseQuestionId)
        {
            return new SqlConfidenceContext().ExerciseQuestions.Find(ExerciseQuestionId);
        }

        public void Save(ExerciseQuestion Question)
        {
            // Does it already exist?
            var exists = Question.ExerciseQuestionId != 0;
            var context = new SqlConfidenceContext();

            // Stop it saving any related objects
            Question.Exercise = null;
            Question.ExerciseQuestionAnswereds = null;
            Question.ExerciseQuestionChoices = null;
            Question.ExerciseQuestionUnitTests = null;

            if(!exists)
            {
                context.ExerciseQuestions.Add(Question);
            }
            else
            {
                context.ExerciseQuestions.Attach(Question);
                context.Entry(Question).State = EntityState.Modified;
            }

            context.SaveChanges();
        }
    }
}
