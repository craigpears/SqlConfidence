using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Models
{
    public class DataSourceModel
    {
        public int DataSourceId { get; set; }
        public String Name { get; set; }
        public List<TableModel> Tables { get; set; }
        public Guid DataSourceIdGuid { get; set; }
    }
}
