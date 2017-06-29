using Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    public class QuestionModel : BaseModel
    {
        public int QuestionId { get; set; }
        public int ExerciseId { get; set; }
        public String Instructions { get; set; }
        public String AnswerTemplate { get; set; }
        public String Description { get; set; }
        public String Hint { get; set; }
        public int Order { get; set; }
        public Boolean Answered { get; set; }
        public DateTime? AnsweredDate { get; set; }
        public String StartingSql { get; set; }
        public int ExerciseQuestionType { get; set; }
        public Guid ExerciseQuestionIdGuid { get; set; }
    }
}