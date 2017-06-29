using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class DataSource
    {
        public DataSource()
        {
            this.DataSourceTables = new List<DataSourceTable>();
            this.Exercises = new List<Exercise>();
        }

        public int DataSourceId { get; set; }
        public string Name { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public bool Deleted { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
        public string DeletedBy { get; set; }
        public System.Guid DataSourceIdGuid { get; set; }
        public virtual ICollection<DataSourceTable> DataSourceTables { get; set; }
        public virtual ICollection<Exercise> Exercises { get; set; }
    }
}
