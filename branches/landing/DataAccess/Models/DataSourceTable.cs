using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class DataSourceTable
    {
        public int DataSourceTableId { get; set; }
        public int DataSourceId { get; set; }
        public string TableName { get; set; }
        public string TableAlias { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public bool Deleted { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
        public string DeletedBy { get; set; }
        public string Columns { get; set; }
        public System.Guid DataSourceTableIdGuid { get; set; }
        public virtual DataSource DataSource { get; set; }
    }
}
