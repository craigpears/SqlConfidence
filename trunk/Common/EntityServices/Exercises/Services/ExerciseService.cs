using Common.DataSources.Interfaces;
using Common.Exercises.Interfaces;
using Common.Models;
using Common.Questions.Interfaces;
using DataAccess.Enums;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Exercises.Services
{
    public class ExerciseService : IExerciseService
    {
        protected IQuestionService _questionService;
        protected IExerciseRepository _exerciseRepository;
        protected IDataSourceService _dataSourceService;

        public ExerciseService(
            IQuestionService QuestionService,
            IExerciseRepository ExerciseRepository,
            IDataSourceService DataSourceService)
        {
            _questionService = QuestionService;
            _exerciseRepository = ExerciseRepository;
            _dataSourceService = DataSourceService;
        }

        public Exercise Get(int Id, UserModel User = null)
        {
            var exercise = _exerciseRepository.Get(Id);
            exercise = this.Create(exercise, User);
            return exercise;
        }

        public List<Exercise> GetAll(UserModel User = null)
        {
            var exercises = _exerciseRepository.GetAll();
            exercises = this.Create(exercises, User);
            return exercises;
        }

        public List<Exercise> GetAllByUpdatedDate()
        {
            var exercises = _exerciseRepository.GetAllByUpdatedDate();
            exercises = this.Create(exercises);
            return exercises;
        }

        public void Save(Exercise Exercise, UserModel user)
        {
            if (String.IsNullOrEmpty(Exercise.CreatedBy))
            {
                Exercise.CreatedBy = user.Email;
                Exercise.CreatedDate = DateTime.Now;
                Exercise.ExerciseIdGuid = Guid.NewGuid();
            }
            else
            {
                Exercise.UpdatedBy = user.Email;
                Exercise.UpdatedDate = DateTime.Now;
            }

            _exerciseRepository.Save(Exercise);
        }

        protected List<Exercise> Create(List<Exercise> Exercises, UserModel User = null)
        {
            foreach (var exercise in Exercises)
            {
                this.Create(exercise, User);
            }
            return Exercises;
        }

        protected Exercise Create(Exercise Exercise, UserModel User = null)
        {
            Exercise.ExerciseQuestions = _questionService.GetByExerciseId(Exercise.ExerciseId, User);
            Exercise.DataSource = _dataSourceService.Get(Exercise.DataSourceId);

            // Link the child objects to the question
            foreach (var item in Exercise.ExerciseQuestions)
                item.Exercise = Exercise;

            return Exercise;
        }
    }
}
