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

namespace Common.Questions.Services
{
    public class QuestionService : IQuestionService
    {
        protected IQuestionRepository _questionRepository;
        protected IQuestionChoicesService _choicesService;
        protected IQuestionAnsweredService _questionAnsweredService;
        protected IQuestionUnitTestsService _unitTestsService;

        public QuestionService(
            IQuestionRepository QuestionRepository,
            IQuestionChoicesService ChoicesService,
            IQuestionAnsweredService QuestionAnsweredService,
            IQuestionUnitTestsService UnitTestsService)
        {
            _questionRepository = QuestionRepository;
            _choicesService = ChoicesService;
            _questionAnsweredService = QuestionAnsweredService;
            _unitTestsService = UnitTestsService;
        }

        public ExerciseQuestion Get(int Id, UserModel User)
        {
            var question = _questionRepository.Get(Id);
            question = this.Create(question, User);
            return question;
        }

        public List<ExerciseQuestion> GetByExerciseId(int ExerciseId, UserModel User)
        {
            var questions = _questionRepository.GetByExerciseId(ExerciseId);
            questions = this.Create(questions, User);
            return questions;
        }

        public void Save(ExerciseQuestion Question, UserModel User)
        {
            if(String.IsNullOrEmpty(Question.CreatedBy))
            {
                Question.CreatedBy = User.Email;
                Question.CreatedDate = DateTime.Now;
                Question.ExerciseQuestionIdGuid = Guid.NewGuid();
                if(Question.Exercise == null)
                {
                    var exerciseService = IocContainer.Container.GetInstance<IExerciseService>();
                    Question.Exercise = exerciseService.Get(Question.ExerciseId, User);
                }
                Question.Order = (Question.Exercise.ExerciseQuestions.Select(x => x.Order).Max() + 1) ?? 0;

                if(Question.ExerciseQuestionType == ExerciseQuestionType.UnitTested)
                {
                    Question.AnswerTemplate = "N/A";
                }
            }
            else
            {
                Question.UpdatedBy = User.Email;
                Question.UpdatedDate = DateTime.Now;
            }

            // Saving the question needs to be done first so that we can get the id back
            // At the moment this will delete all navigation properties though, so take a reference to these lists so they can be restored after
            var questionChoices = Question.ExerciseQuestionChoices;
            var questionUnitTests = Question.ExerciseQuestionUnitTests;
            _questionRepository.Save(Question);
            Question.ExerciseQuestionChoices = questionChoices;
            Question.ExerciseQuestionUnitTests = questionUnitTests;

            foreach (var test in Question.ExerciseQuestionUnitTests)
            {
                test.ExerciseQuestionId = Question.ExerciseQuestionId;
                _unitTestsService.Save(test, User);
            }

            foreach (var choice in Question.ExerciseQuestionChoices)
            {
                choice.ExerciseQuestionId = Question.ExerciseQuestionId;
                _choicesService.Save(choice, User);
            }
        }

        protected List<ExerciseQuestion> Create(List<ExerciseQuestion> Questions, UserModel User = null)
        {
            foreach (var question in Questions)
            {
                this.Create(question, User);
            }
            return Questions;
        }

        protected ExerciseQuestion Create(ExerciseQuestion Question, UserModel User = null)
        {
            if (User != null)
            {
                Question.ExerciseQuestionAnswereds = _questionAnsweredService.GetByQuestionId(Question.ExerciseQuestionId, User.UserId);
            }

            if(Question.ExerciseQuestionType == ExerciseQuestionType.MultipleChoice)
            {
                Question.ExerciseQuestionChoices = _choicesService.GetByQuestionId(Question.ExerciseQuestionId);
            }
            else if(Question.ExerciseQuestionType == ExerciseQuestionType.UnitTested)
            {
                Question.ExerciseQuestionUnitTests = _unitTestsService.GetByQuestionId(Question.ExerciseQuestionId);
            }

            // Link the child objects to the question
            foreach (var item in Question.ExerciseQuestionUnitTests)
                item.ExerciseQuestion = Question;

            foreach (var item in Question.ExerciseQuestionAnswereds)
                item.ExerciseQuestion = Question;

            foreach (var item in Question.ExerciseQuestionChoices)
                item.ExerciseQuestion = Question;

            // Put the answered information into the question model
            if(User != null)
            {
                Question.Answered = Question.ExerciseQuestionAnswereds.Any();
                if(Question.Answered)
                    Question.AnsweredDate = Question.ExerciseQuestionAnswereds.Select(x => x.CreatedDate).Min();
            }

            return Question;
        }
    }
}
