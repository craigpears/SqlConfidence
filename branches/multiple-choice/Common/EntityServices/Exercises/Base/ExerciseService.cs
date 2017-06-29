using Common.EntityServices.Exercises.MultipleChoice;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.EntityServices.Exercises.Base
{
    public class ExerciseService:IEntityService<Exercise>, IExerciseService
    {
        IMultipleChoiceExerciseService _multipleChoiceService;
        IExerciseRepository _exerciseRepository;

        public ExerciseService(IMultipleChoiceExerciseService multipleChoiceService, IExerciseRepository exerciseRepository)
        {
            _multipleChoiceService = multipleChoiceService;
            _exerciseRepository = exerciseRepository;
        }

        public Exercise Get(int id)
        {
            return this.Get(id, null);
        }

        public Exercise Get(int id, User user)
        {
            var mutlipleChoiceExercise = _multipleChoiceService.Get(id, user);

            if (mutlipleChoiceExercise != null)
                return mutlipleChoiceExercise;

            return null;
        }

        public List<Exercise> GetAll()
        {
            return this.GetAll(null);
        }

        public List<Exercise> GetAll(User user)
        {
            var exercises = new List<Exercise>();
            var multipleChoiceExercises = _multipleChoiceService.GetAll(user);
            exercises.AddRange(multipleChoiceExercises);

            return exercises;
        }

        public Exercise GetByQuestion(int id, User user)
        {
            var exerciseId = _exerciseRepository.GetExerciseIdFromQuestionId(id);
            return this.Get(exerciseId, user);
        }

        public void MarkQuestionAsAnswered(int id, User user)
        {
            _exerciseRepository.MarkQuestionAsAnswered(id, user.UserId);
        }
    }
}
