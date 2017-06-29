using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models
{
    public partial class DataSource:EntityBase
    {
        public DataSource()
        {
            this.DataSourceTables = new List<DataSourceTable>();
            this.Exercises = new List<Exercise>();
        }

        [Key]
        public int DataSourceId { get; set; }
        public string Name { get; set; }
        public Guid DataSourceIdGuid { get; set; }
        public virtual ICollection<DataSourceTable> DataSourceTables { get; set; }
        public virtual ICollection<Exercise> Exercises { get; set; }
    }
}
