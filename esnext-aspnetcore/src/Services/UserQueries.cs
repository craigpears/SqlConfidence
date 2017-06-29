using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Data.SqlClient;
using ViewModels;

namespace Services
{
    public class UserQueries
    {
        protected String _connectionString;

        public UserQueries(string connectionString)
        {
            _connectionString = connectionString;
        }

        public UserQueryResult Executequery(string query)
        {
            ValidateQuery(query);
            var result = new UserQueryResult
            {
                ColumnHeaders = new List<String>(),
                Data = new List<List<String>>()
            };

            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                new SqlCommand("BEGIN TRANSACTION", conn).ExecuteNonQuery();
                using (var resultReader = new SqlCommand(query, conn).ExecuteReader())
                {
                    for (int i = 0; i < resultReader.FieldCount; i++)
                    {
                        result.ColumnHeaders.Add(resultReader.GetName(i));
                    }

                    while (resultReader.Read())
                    {
                        var dataRow = new List<String>();
                        foreach (var column in result.ColumnHeaders)
                        {
                            dataRow.Add(resultReader[column].ToString());
                        }
                        result.Data.Add(dataRow);
                    }
                }

                new SqlCommand("ROLLBACK TRANSACTION", conn).ExecuteNonQuery();
            }

            return result;
        }

        private void ValidateQuery(String query)
        {
            query = query.ToLower();
            if (query.Contains("transaction")) throw new Exception("User queries must not contain transactions");
            if (query.Contains("drop")) throw new Exception("User queries must not contain drop");
            // Allow delete in a word, but not the command itself
            if (Regex.IsMatch(query, @"\bdelete\b", RegexOptions.IgnoreCase)) throw new Exception("User queries must not contain delete");
            if (query.Contains("rollback")) throw new Exception("User queries must not contain transactions");
            if (query.Contains("commit")) throw new Exception("User queries must not contain transactions");
            if (query.Contains("bulk insert")) throw new Exception("User queries must not contain bulk inserts");
            if (query.Contains("cursor")) throw new Exception("User queries must not contain bulk cursors");
            if (query.Contains("truncate")) throw new Exception("User queries must not contain truncate commands");
            if (query.Contains("alter") && query.Contains("table")) throw new Exception("User queries must not alter tables");
            if (query.Contains("create") && query.Contains("table")) throw new Exception("User queries must not create tables");
            if (query.Contains("grant") || query.Contains("revoke")) throw new Exception("User queries must not alter user permissions");
            if (Regex.IsMatch(query, @"\buse\b", RegexOptions.IgnoreCase)) throw new Exception("User queries must not contain use statements");
            if (query.Contains("information_schema")) throw new Exception("User queries must not access any information schema objects");
            if (query.Contains("sys")) throw new Exception("User queries must not access any system tables or views");
            if (query.Contains("constraint")) throw new Exception("User queries must not include constraints");
            if (query.Contains("tempdb")) throw new Exception("User queries must not use tempdb");
            if (query.Contains("spt_values")) throw new Exception("User queries must not use spt_values");
            if (query.Contains("exec")) throw new Exception("User queries must not use exec");
            if (query.Contains("while")) throw new Exception("User queries must not use while");
            if (query.Contains("wait")) throw new Exception("User queries must not use wait");
            if (query.Contains("delay")) throw new Exception("User queries must not use delay");
            if (query.Contains("maxrecursion")) throw new Exception("User queries must not use maxrecursion");

            var strippedquery = Regex.Replace(query, "[^a-z0-9% ._]", string.Empty);
            String[] parts = strippedquery.Split(' ');

            if (parts.Where(x => x.Contains("join")).Count() > 5) throw new Exception("User queries must not contain large numbers of joins");
            if (parts.Where(x => x.Contains("union")).Count() > 5) throw new Exception("User queries must not contain large numbers of unions");
        }
    }
}
