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
    public class QuestionChoicesService : IQuestionChoicesService
    {
        protected IQuestionChoicesRepository _questionChoicesRepository { get; set; }

        public QuestionChoicesService(IQuestionChoicesRepository QuestionChoicesRepository)
        {
            _questionChoicesRepository = QuestionChoicesRepository;
        }

        public ExerciseQuestionChoice Get(Guid Id)
        {
            var choice = _questionChoicesRepository.Get(Id);
            choice = this.Create(choice);
            return choice;
        }

        public List<ExerciseQuestionChoice> GetByQuestionId(int Id)
        {
            var choices = _questionChoicesRepository.GetByQuestionId(Id);
            choices = this.Create(choices);
            return choices;
        }

        public void Save(ExerciseQuestionChoice Choice, UserModel user)
        {
            _questionChoicesRepository.Save(Choice);
        }

        protected List<ExerciseQuestionChoice> Create(List<ExerciseQuestionChoice> Choices)
        {
            foreach (var choice in Choices)
            {
                this.Create(choice);
            }
            return Choices;
        }

        protected ExerciseQuestionChoice Create(ExerciseQuestionChoice Choice)
        {
            return Choice;
        }
    }
}
