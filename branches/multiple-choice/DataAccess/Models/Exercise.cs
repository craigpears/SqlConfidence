using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models
{
    public class Exercise:EntityBase
    {
        public Exercise()
        {
            this.ExerciseQuestions = new List<ExerciseQuestion>();
        }
        [Key]
        public int ExerciseId { get; set; }
        public int DataSourceId { get; set; }
        public string Name { get; set; }
        public string Summary { get; set; }
        public Nullable<Boolean> Published { get; set; }
        public Nullable<DateTime> PublishedDate { get; set; }
        public string PublishedBy { get; set; }
        public String SectionName { get; set; }
        public int Order { get; set; }
        public Nullable<Boolean> ShowQueryBuilder { get; set; }
        public Guid ExerciseIdGuid { get; set; }
        public virtual DataSource DataSource { get; set; }
        public virtual ICollection<ExerciseQuestion> ExerciseQuestions { get; set; }
    }
}
