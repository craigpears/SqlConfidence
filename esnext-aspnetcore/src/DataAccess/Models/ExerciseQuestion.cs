using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;

namespace DataAccess.Models
{
    [DataContract]
    public abstract class ExerciseQuestion : EntityBase
    {
        public ExerciseQuestion()
        {
            this.ExerciseQuestionAnswereds = new List<ExerciseQuestionAnswered>();
        }
        [Key]
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Instructions { get; set; }

        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public Nullable<int> Order { get; set; }
        [NotMapped]
        [DataMember]
        public Boolean Answered {
            get {
                return this.ExerciseQuestionAnswereds.Any();
            }
        }
        [NotMapped]
        [DataMember]
        public Nullable<DateTime> AnsweredDate
        {
            get
            {
                if (!Answered) return null;
                return this.ExerciseQuestionAnswereds.OrderByDescending(x => x.CreatedDate).First().CreatedDate;
            }
        }

        [DataMember]
        public virtual ICollection<ExerciseQuestionAnswered> ExerciseQuestionAnswereds { get; set; }
    }
}
