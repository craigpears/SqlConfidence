using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class MultipleChoiceDataQuery
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public int ExerciseQuestionId { get; set; }
        public String SqlQuery { get; set; }
        [ForeignKey("ExerciseQuestionId")]
        [JsonIgnore]
        public virtual MultipleChoiceQuestion Question { get; set; }
    }
}
