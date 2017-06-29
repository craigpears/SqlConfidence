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
    public class QuestionAnsweredRepository : IQuestionAnsweredRepository
    {
        public List<ExerciseQuestionAnswered> GetAll()
        {
            return new SqlConfidenceContext().ExerciseQuestionAnswereds.ToList();
        }

        public List<ExerciseQuestionAnswered> GetByQuestionId(int Id, int UserId)
        {
            return new SqlConfidenceContext().ExerciseQuestionAnswereds.Where(x => x.ExerciseQuestionId == Id && x.UserId == UserId).ToList();
        }

        public ExerciseQuestionAnswered Get(int Id)
        {
            return new SqlConfidenceContext().ExerciseQuestionAnswereds.Find(Id);
        }

        public void Save(ExerciseQuestionAnswered QuestionAnswered)
        {
            // Does it already exist?
            var exists = QuestionAnswered.ExerciseQuestionAnsweredId != 0;
            var context = new SqlConfidenceContext();

            // Stop it saving any related objects
            QuestionAnswered.ExerciseQuestion = null;

            if(!exists)
            {
                context.ExerciseQuestionAnswereds.Add(QuestionAnswered);
            }
            else
            {
                context.ExerciseQuestionAnswereds.Attach(QuestionAnswered);
                context.Entry(QuestionAnswered).State = EntityState.Modified;
            }

            context.SaveChanges();
        }
    }
}
