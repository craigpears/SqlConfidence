using Common.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Common
{
    public interface ISourceDataAccess
    {
        DataTable GetDataTable(string Query, SqlConnection conn = null, int MaxRows = 0);
        void BeginUserTransaction(SqlConnection conn);
        void RollBackUserTransaction(SqlConnection conn);
        void ExecuteUserQuery(string Query, SqlConnection conn = null);
        List<TableModel> ListAllTables();
    }
}
