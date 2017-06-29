using DataAccess.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Models
{
    public partial class ExerciseQuestion
    {
        public ExerciseQuestion()
        {
            this.ExerciseQuestionAnswereds = new List<ExerciseQuestionAnswered>();
            this.ExerciseQuestionChoices = new List<ExerciseQuestionChoice>();
            this.ExerciseQuestionUnitTests = new List<ExerciseQuestionUnitTest>();
        }

        public int ExerciseQuestionId { get; set; }
        public int ExerciseId { get; set; }
        public string InstructionsTemplate { get; set; }
        public string AnswerTemplate { get; set; }
        public string Description { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public bool Deleted { get; set; }
        public string DeletedBy { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
        public string Hint { get; set; }
        public Nullable<int> Order { get; set; }
        public string StartingSql { get; set; }
        public System.Guid ExerciseQuestionIdGuid { get; set; }
        public ExerciseQuestionType ExerciseQuestionType { get; set; }
        [NotMapped]
        public Boolean Answered { get; set; }
        [NotMapped]
        public Nullable<DateTime> AnsweredDate { get; set; }
        public virtual Exercise Exercise { get; set; }
        public virtual ICollection<ExerciseQuestionAnswered> ExerciseQuestionAnswereds { get; set; }
        public virtual ICollection<ExerciseQuestionChoice> ExerciseQuestionChoices { get; set; }
        public virtual ICollection<ExerciseQuestionUnitTest> ExerciseQuestionUnitTests { get; set; }
    }
}
