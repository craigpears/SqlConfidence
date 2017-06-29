using Common.DataSources.Interfaces;
using DataAccess.Models;
using System.Collections.Generic;
using System.Linq;

namespace Common.EntityServices.Exercises.MultipleChoice
{
    public class MultipleChoiceExerciseService : IEntityService<MultipleChoiceExercise>, IMultipleChoiceExerciseService
    {
        protected IMultipleChoiceExerciseRepository _repository { get; set;}
        protected IMultipleChoiceQuestionRepository _questionRepository { get; set; }
        protected IDataSourceService _dataSourceService { get; set; }

        public MultipleChoiceExerciseService(IMultipleChoiceExerciseRepository repository,
            IMultipleChoiceQuestionRepository questionRepository,
            IDataSourceService dataSourceService)
        {
            _repository = repository;
            _questionRepository = questionRepository;
            _dataSourceService = dataSourceService;
        }

        public MultipleChoiceExercise Get(int id)
        {
            return Create(_repository.Get(id), null);
        }

        public MultipleChoiceExercise Get(int id, User user)
        {
            return Create(_repository.Get(id), user);
        }

        public List<MultipleChoiceExercise> GetAll()
        {
            return this.GetAll(null);
        }

        public List<MultipleChoiceExercise> GetAll(User user)
        {
            var exercises = _repository.GetAll().ToList();
            foreach (var exercise in exercises)
            {
                this.Create(exercise, user);
            }
            return exercises;
        }

        protected List<MultipleChoiceExercise> Create(List<MultipleChoiceExercise> exercises, User user)
        {
            foreach (var exercise in exercises)
                this.Create(exercise, user);

            return exercises;
        }

        protected MultipleChoiceExercise Create(MultipleChoiceExercise exercise, User user)
        {
            exercise.DataSource = _dataSourceService.Get(exercise.DataSourceId);
            var questions = _questionRepository.GetByExercise(exercise.ExerciseId);
            exercise.Questions = questions;
            questions.ForEach(x => exercise.ExerciseQuestions.Add(x));

            return exercise;
        }
    }
}
