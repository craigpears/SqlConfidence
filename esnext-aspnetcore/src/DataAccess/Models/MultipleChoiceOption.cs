using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class MultipleChoiceOption
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public int ExerciseQuestionId { get; set; }

        [MaxLength(20)]
        public string Description { get; set; }
        public string CorrectAnswerMessage { get; set; }
        public string IncorrectAnswerMessage { get; set; }
        
        [ForeignKey("ExerciseQuestionId")]
        [JsonIgnore]
        public virtual MultipleChoiceQuestion ExerciseQuestion { get; set; }
    }
}
