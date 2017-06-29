using Common.Models;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Common.QueryBuilder
{
    public class TSQLQueryBuilder : IQueryBuilder
    {
        public String BuildQuery(String BaseCommand, Exercise Exercise, ExerciseQuestion Question)
        {
            String query = BaseCommand.ToLower();
            Regex regex;
            
            // Handle table alias's
            foreach (var table in Exercise.DataSource.DataSourceTables)
            {
                String alias = table.TableAlias.ToLower();
                String tableName = table.TableName.ToLower();
                // Only replace alias at word boundaries
                String replacePattern = String.Format(@"\b{0}\b", alias);
                query = Regex.Replace(query, replacePattern, tableName, RegexOptions.IgnoreCase);
            }

            return query;
        }
    }
}
