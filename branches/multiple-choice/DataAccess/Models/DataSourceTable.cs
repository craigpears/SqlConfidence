using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models
{
    public partial class DataSourceTable:EntityBase
    {
        [Key]
        public int DataSourceTableId { get; set; }
        public int DataSourceId { get; set; }
        public string TableName { get; set; }
        public string TableAlias { get; set; }
        public string Columns { get; set; }
        public Guid DataSourceTableIdGuid { get; set; }
        public virtual DataSource DataSource { get; set; }
    }
}
