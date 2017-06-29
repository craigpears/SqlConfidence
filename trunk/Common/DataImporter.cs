using GenericParsing;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Common
{
    public class DataImporter
    {
        public static DataTable CsvToDataTable(String filepath)
        {
            using (GenericParserAdapter parser = new GenericParserAdapter(filepath))
            {
                parser.ColumnDelimiter = ',';
                parser.FirstRowHasHeader = true;
                parser.TextQualifier = '\"';

                DataTable dt = parser.GetDataTable();
                foreach (DataColumn column in dt.Columns)
                {
                    TranslateColumnName(column);
                }
                return dt;
            }
        }

        public static void TranslateColumnName(DataColumn column)
        {
            // Do something here
            // e.g. column.ColumnName = "Bob";
            // e.g. column.ColumnName = column.ColumnName.Replace(" ", "_") - replace spaces with underscores
            // e.g. column.ColumnName = column.ColumnName.Replace(".", ""); - remove all dots
            // e.g. column.ColumnName = column.ColumnName.Replace("(", ""); - remove all opening brackets
            // e.g. column.ColumnName = column.ColumnName.Replace(")", ""); - remove all close brackets

            column.ColumnName = column.ColumnName.Replace(" ", "_");
            column.ColumnName = column.ColumnName.Replace(".", "");
            column.ColumnName = column.ColumnName.Replace("(", "");
            column.ColumnName = column.ColumnName.Replace(")", "");
        }

        public static void CreateTable(DataTable source, String TableName)
        {
            using (SqlConnection conn = new SqlConnection(Properties.Settings.Default.DatabaseDataSourcesConnectionString))
            {
                conn.Open();
                try
                {
                    TableName = TableName.Replace(".csv", "");
                    // Guard against sql injection
                    Regex validNamePattern = new Regex("^[a-zA-Z_0-9]+$");
                    if (!validNamePattern.IsMatch(TableName))
                    {
                        throw new Exception("Table names must contain only letters, underscores and numbers");
                    }

                    String createTableCommand = "CREATE TABLE " + TableName + "(";
                    int columnNo = 0;
                    foreach (DataColumn column in source.Columns)
                    {
                        if (!validNamePattern.IsMatch(column.ColumnName))
                        {
                            throw new Exception("Column names must contain only letters, underscores and numbers");
                        }
                        //int.TryParse
                        //bool.TryParse
                        //float.tryparse
                        //date.tryparse
                        var columnIsInteger = true;
                        for (var i = 0; i < source.Rows.Count && i < 1000; i++ )
                        {
                            var row = source.Rows[i];
                            if (columnIsInteger != false)
                            {
                                int intOut;
                                var isInt = int.TryParse(row.ItemArray[columnNo].ToString(), out intOut);
                                if (!isInt)
                                {
                                    columnIsInteger = false;
                                }
                            }
                        }
                        
                        // if is integer
                        // add sql syntax to create an int column
                        // else
                        // {
                        createTableCommand += column.ColumnName + " nvarchar(max) ";
                        // }


                        if (column != source.Columns[source.Columns.Count - 1]) createTableCommand += ",";

                        columnNo++;
                    }
                    createTableCommand += ");";

                    using (SqlCommand command = new SqlCommand(createTableCommand, conn))
                    {
                        command.ExecuteNonQuery();
                    }

                    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(conn))
                    {
                        bulkCopy.DestinationTableName = TableName;
                        bulkCopy.WriteToServer(source);
                    }
                }
                catch (Exception)
                {
                    
                    throw;
                }
                finally
                {
                    conn.Close();
                }
            }
        }
    }
}
