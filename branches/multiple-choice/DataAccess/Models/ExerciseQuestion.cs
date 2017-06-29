using DataAccess.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace DataAccess.Models
{
    public class ExerciseQuestion : EntityBase
    {
        public ExerciseQuestion()
        {
            this.ExerciseQuestionAnswereds = new List<ExerciseQuestionAnswered>();
            this.ExerciseQuestionChoices = new List<ExerciseQuestionChoice>();
            this.ExerciseQuestionUnitTests = new List<ExerciseQuestionUnitTest>();
        }
        [Key]
        public int ExerciseQuestionId { get; set; }
        public int ExerciseId { get; set; }
        public string InstructionsTemplate { get; set; }
        public string AnswerTemplate { get; set; }
        public string Description { get; set; }
        public string Hint { get; set; }
        public Nullable<int> Order { get; set; }
        public string StartingSql { get; set; }
        public ExerciseQuestionType ExerciseQuestionType { get; set; }
        [NotMapped]
        public Boolean Answered {
            get {
                return this.ExerciseQuestionAnswereds.Any();
            }
        }
        [NotMapped]
        public Nullable<DateTime> AnsweredDate
        {
            get
            {
                if (!Answered) return null;
                return this.ExerciseQuestionAnswereds.OrderByDescending(x => x.CreatedDate).First().CreatedDate;
            }
        }

        public virtual Exercise Exercise { get; set;}

        public virtual ICollection<ExerciseQuestionAnswered> ExerciseQuestionAnswereds { get; set; }
        public virtual ICollection<ExerciseQuestionChoice> ExerciseQuestionChoices { get; set; }
        public virtual ICollection<ExerciseQuestionUnitTest> ExerciseQuestionUnitTests { get; set; }
    }
}
