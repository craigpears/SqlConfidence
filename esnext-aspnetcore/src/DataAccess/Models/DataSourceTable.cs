using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace DataAccess.Models
{
    public partial class DataSourceTable:EntityBase
    {
        [Key]
        public int Id { get; set; }
        public int DataSourceId { get; set; }

        public string TableName { get; set; }
        [JsonIgnore]
        public virtual DataSource DataSource { get; set; }
    }
}
