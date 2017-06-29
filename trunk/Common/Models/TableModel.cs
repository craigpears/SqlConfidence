using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    public class TableModel
    {
        public int TableId { get; set; }
        public int DataSourceId { get; set; }
        public String TableName { get; set; }
        public String Alias { get; set; }
        public List<ColumnModel> Columns { get; set; }
        public Guid DataSourceTableIdGuid { get; set; }
    }
}
