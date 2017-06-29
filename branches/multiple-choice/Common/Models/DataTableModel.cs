using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Models
{
    public class DataTableModel
    {
        public List<ColumnModel> Columns { get; set; }
        public List<Dictionary<String, Object>> Rows { get; set; }

        public DataTableModel()
        {
            Columns = new List<ColumnModel>();
            Rows = new List<Dictionary<String, Object>>();
        }
    }
}
