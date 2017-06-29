using Common.Models;
using Common.Questions.Interfaces;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Questions.Services
{
    public class QuestionAnsweredService : IQuestionAnsweredService
    {
        protected IQuestionAnsweredRepository _questionAnsweredRepository { get; set; }

        public QuestionAnsweredService(IQuestionAnsweredRepository QuestionAnsweredRepository)
        {
            _questionAnsweredRepository = QuestionAnsweredRepository;
        }

        public ExerciseQuestionAnswered Get(int Id)
        {
            var questionAnswered = _questionAnsweredRepository.Get(Id);
            questionAnswered = this.Create(questionAnswered);
            return questionAnswered;
        }

        public List<ExerciseQuestionAnswered> GetByQuestionId(int Id, int UserId)
        {
            var questionAnswereds = _questionAnsweredRepository.GetByQuestionId(Id, UserId);
            questionAnswereds = this.Create(questionAnswereds);
            return questionAnswereds;
        }

        public void Save(ExerciseQuestionAnswered QuestionAnswered, UserModel user)
        {
            _questionAnsweredRepository.Save(QuestionAnswered);
        }

        protected List<ExerciseQuestionAnswered> Create(List<ExerciseQuestionAnswered> QuestionAnswereds)
        {
            foreach (var questionAnswered in QuestionAnswereds)
            {
                this.Create(questionAnswered);
            }
            return QuestionAnswereds;
        }

        protected ExerciseQuestionAnswered Create(ExerciseQuestionAnswered QuestionAnswered)
        {
            return QuestionAnswered;
        }
    }
}
