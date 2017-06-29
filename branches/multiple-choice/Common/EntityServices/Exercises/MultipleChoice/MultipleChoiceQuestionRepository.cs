using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.EntityServices.Exercises.MultipleChoice
{
    public class MultipleChoiceQuestionRepository: EntityRepository<MultipleChoiceQuestion>, IMultipleChoiceQuestionRepository
    {
        protected SqlConfidenceContext _context;
        public MultipleChoiceQuestionRepository()
        {
            _context = new SqlConfidenceContext();
            base.Includes.Add("Options");
            base.Includes.Add("DataQueries");
            base.Includes.Add("ExerciseQuestionAnswereds");
        }

        public List<MultipleChoiceQuestion> GetByExercise(int id)
        {
            return base.GetAll().Where(x => x.ExerciseId == id).ToList();
        }
    }
}
