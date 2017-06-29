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
    public class QuestionChoicesRepository : IQuestionChoicesRepository
    {
        public List<ExerciseQuestionChoice> GetAll()
        {
            return new SqlConfidenceContext().ExerciseQuestionChoices.ToList();
        }

        public List<ExerciseQuestionChoice> GetByQuestionId(int Id)
        {
            return new SqlConfidenceContext().ExerciseQuestionChoices.Where(x => x.ExerciseQuestionId == Id).ToList();
        }

        public ExerciseQuestionChoice Get(Guid Id)
        {
            return new SqlConfidenceContext().ExerciseQuestionChoices.Find(Id);
        }

        public void Save(ExerciseQuestionChoice Choice)
        {
            // Does it already exist?
            var exists = Choice.ExerciseQuestionChoiceId != Guid.Empty;
            var context = new SqlConfidenceContext();

            // Stop it saving any related objects
            Choice.ExerciseQuestion = null;

            if(!exists)
            {
                Choice.ExerciseQuestionChoiceId = Guid.NewGuid();
                context.ExerciseQuestionChoices.Add(Choice);
            }
            else
            {
                context.ExerciseQuestionChoices.Attach(Choice);
                context.Entry(Choice).State = EntityState.Modified;
            }

            context.SaveChanges();
        }
    }
}
