using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public class Exercise
    {
        public Exercise()
        {
            this.ExerciseQuestions = new List<ExerciseQuestion>();
        }

        public int ExerciseId { get; set; }
        public int DataSourceId { get; set; }
        public string Name { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public bool Deleted { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
        public string DeletedBy { get; set; }
        public Nullable<bool> Published { get; set; }
        public Nullable<System.DateTime> PublishedDate { get; set; }
        public string PublishedBy { get; set; }
        public String SectionName { get; set; }
        public int Order { get; set; }
        public Nullable<bool> ShowQueryBuilder { get; set; }
        public System.Guid ExerciseIdGuid { get; set; }
        public virtual DataSource DataSource { get; set; }
        public virtual ICollection<ExerciseQuestion> ExerciseQuestions { get; set; }
    }
}
