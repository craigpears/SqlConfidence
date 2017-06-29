using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Common
{
    public class DAHelpers
    {
        /// <summary>
        /// Takes a string from a reader and checks if it was db null or empty
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static Boolean IsDBNullOrEmpty(Object obj)
        {
            if (obj == DBNull.Value) return true;
            if (String.IsNullOrEmpty((string)obj)) return true;
            return false;
        }

        public static bool HasColumn(IDataRecord dr, string columnName)
        {
            for (int i = 0; i < dr.FieldCount; i++)
            {
                if (dr.GetName(i).Equals(columnName, StringComparison.InvariantCultureIgnoreCase))
                    return true;
            }
            return false;
        }

        public static bool BooleanFromReader(IDataRecord dr, string columnName)
        {
            if (!HasColumn(dr, columnName)) return false;
            if (dr[columnName] == DBNull.Value) return false;

            return (Boolean)dr[columnName];
        }

        public static DateTime DateTimeFromReader(IDataRecord dr, string columnName)
        {
            if (!HasColumn(dr, columnName)) return new DateTime();
            if (dr[columnName] == DBNull.Value) return new DateTime();

            return (DateTime)dr[columnName];
        }

        public static DateTime? NullableDateTimeFromReader(IDataRecord dr, string columnName)
        {
            if (!HasColumn(dr, columnName)) return new DateTime();
            if (dr[columnName] == DBNull.Value) return null;

            return (DateTime)dr[columnName];
        }

        public static String StringFromReader(IDataRecord dr, string columnName)
        {
            if (!HasColumn(dr, columnName)) return "";
            if (dr[columnName] == DBNull.Value) return "";

            return (String)dr[columnName];
        }

        public static String NullableStringFromReader(IDataRecord dr, string columnName)
        {
            if (!HasColumn(dr, columnName)) return "";
            if (dr[columnName] == DBNull.Value) return null;

            return (String)dr[columnName];
        }

    }
}
