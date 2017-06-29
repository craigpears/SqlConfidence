using Common.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Common
{
    internal class TSQLDataAccess:ISourceDataAccess
    {
        protected Boolean _transactionOpen = false;

        public TSQLDataAccess()
        {
        }

        public void BeginUserTransaction(SqlConnection conn)
        {
            if (conn.State != ConnectionState.Open) throw new Exception("Rollback user transaction - connection supplied is not open");
            using (SqlCommand command = new SqlCommand("BEGIN TRANSACTION", conn))
            {
                command.ExecuteNonQuery();
                _transactionOpen = true;
            }
        }

        public void RollBackUserTransaction(SqlConnection conn)
        {
            if (!_transactionOpen) return;
            
            if (conn.State != ConnectionState.Open) throw new Exception("Rollback user transaction - connection supplied is not open");
            using (SqlCommand command = new SqlCommand("ROLLBACK TRANSACTION", conn))
            {
                command.ExecuteNonQuery();
            }

            _transactionOpen = false;
        }

        public void ExecuteUserQuery(String Query, SqlConnection conn = null)
        {
            if (!_transactionOpen) throw new Exception("Error executing user query - there is no transaction open");
            if (conn != null)
            {
                using (SqlCommand command = new SqlCommand(Query, conn))
                {
                    command.CommandTimeout = 15;
                    command.ExecuteNonQuery();
                }
            }
            else
            {
                using (conn = new SqlConnection(Properties.Settings.Default.DatabaseDataSourcesConnectionString))
                {
                    conn.Open();
                    using (SqlCommand command = new SqlCommand(Query, conn))
                    {
                        command.CommandTimeout = 15;
                        command.ExecuteNonQuery();
                    }
                }
            }
        }

        public DataTable GetDataTable(string Query, SqlConnection conn = null, int MaxRows = 0)
        {
            DataTable dt = new DataTable();
            if (conn != null)
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter(Query, conn))
                {
                    if(MaxRows == 0)
                    {
                        adapter.Fill(dt);
                    }
                    else
                    {
                        adapter.Fill(0, 100, dt);
                    }
                    
                }
            }
            else
            {
                using (conn = new SqlConnection(Properties.Settings.Default.DatabaseDataSourcesConnectionString))
                {
                    conn.Open();
                    using (SqlDataAdapter adapter = new SqlDataAdapter(Query, conn))
                    {
                        if (MaxRows == 0)
                        {
                            adapter.Fill(dt);
                        }
                        else
                        {
                            adapter.Fill(0, 100, dt);
                        }
                    }
                }
            }

            return dt;
        }

        public List<TableModel> ListAllTables()
        {
            List<TableModel> tables = new List<TableModel>();

            using (SqlConnection conn = new SqlConnection(Properties.Settings.Default.DatabaseDataSourcesConnectionString))
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand("select table_name from information_schema.tables where table_catalog = 'SqlConfidenceData'", conn))
                {
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        TableModel model = new TableModel();
                        model.TableName = (String)reader["table_name"];
                        tables.Add(model);
                    }
                    reader.Close();
                }

                foreach (var table in tables)
                {
                    using (SqlCommand command = new SqlCommand("select column_name, data_type from information_Schema.columns where table_name = @TableName", conn))
                    {
                        command.Parameters.AddWithValue("TableName", table.TableName);
                        SqlDataReader reader = command.ExecuteReader();
                        table.Columns = new List<ColumnModel>();
                        while (reader.Read())
                        {
                            ColumnModel column = new ColumnModel();
                            column.Name = (String)reader["column_name"];
                            column.Type = (String)reader["data_type"];
                            table.Columns.Add(column);
                        }
                        reader.Close();
                    }
                }
                
            }

            return tables;
        }
    }
}
