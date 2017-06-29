using Common.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.BusinessLogicServices
{
    public class DataTableModelHelper
    {
        public static DataTableModel DataTableToModel(DataTable dataTable)
        {
            DataTableModel dtModel = new DataTableModel();
            foreach (DataColumn column in dataTable.Columns)
            {
                dtModel.Columns.Add(new ColumnModel()
                {
                    Type = column.DataType.ToString(),
                    Name = column.ColumnName
                });
            }

            foreach (DataRow row in dataTable.Rows)
            {
                Dictionary<String, Object> rowsWithColumnNames = new Dictionary<String, Object>();
                for (var i = 0; i < row.ItemArray.Length; i++)
                {
                    rowsWithColumnNames.Add(dataTable.Columns[i].ColumnName.ToUpper(), row.ItemArray[i]);
                }
                dtModel.Rows.Add(rowsWithColumnNames);
            }
            return dtModel;
        }
    }
}
