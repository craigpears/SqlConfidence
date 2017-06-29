using Common.Exercises.Interfaces;
using Common.Questions.Interfaces;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Exercises.Repositories
{
    public class ExerciseRepository : IExerciseRepository
    {
        protected SqlConfidenceContext _context = new SqlConfidenceContext();
        public Exercise Get(int Id)
        {
            return _context.Exercises.Find(Id);
        }

        public List<Exercise> GetAll()
        {
            return _context.Exercises
                .Where(x => !x.Deleted)
                .OrderBy(x => x.SectionName)
                .ThenBy(x => x.Order)
                .ToList();
        }

        public List<Exercise> GetAllByUpdatedDate()
        {
            return _context.Exercises
                .Where(x => !x.Deleted)
                .OrderByDescending(x => x.UpdatedDate ?? x.CreatedDate)
                .ToList();
        }

        public void Save(Exercise Exercise)
        {
            // Does it already exist?
            var exists = Exercise.ExerciseId != 0;

            // Stop it saving any related objects
            Exercise.ExerciseQuestions = null;
            Exercise.DataSource = null;

            if (!exists)
            {
                _context.Exercises.Add(Exercise);
            }
            else
            {
                _context.Exercises.Attach(Exercise);
                _context.Entry(Exercise).State = EntityState.Modified;
            }

            _context.SaveChanges();
        }
    }
}
