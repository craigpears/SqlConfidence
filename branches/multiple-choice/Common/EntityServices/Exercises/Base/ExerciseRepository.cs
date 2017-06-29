using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.EntityServices.Exercises.Base
{
    public class ExerciseRepository:IExerciseRepository
    {
        protected SqlConfidenceContext _context;
        public ExerciseRepository()
        {
            _context = new SqlConfidenceContext();
        }

        public int GetExerciseIdFromQuestionId(int id)
        {
            return _context.ExerciseQuestions.Find(id).ExerciseId;
        }

        public void MarkQuestionAsAnswered(int id, int userId)
        {
            var answeredEntity = new ExerciseQuestionAnswered()
            {
                UserId = userId,
                CreatedDate = DateTime.Now,
                ExerciseQuestionId = id
            };
            _context.ExerciseQuestionAnswereds.Add(answeredEntity);
            _context.SaveChanges();
        }
    }
}
