using Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Common.QueryBuilder
{
    public class TSQLQueryBuilder : IQueryBuilder
    {
        public String BuildQuery(String BaseCommand, ExerciseModel Exercise, QuestionModel Question)
        {
            String query = BaseCommand.ToLower();
            Regex regex;
            
            // Handle table alias's
            foreach (TableModel table in Exercise.DataSource.Tables)
            {
                String alias = table.Alias.ToLower();
                String tableName = table.TableName.ToLower();
                // Only replace alias at word boundaries
                String replacePattern = String.Format(@"\b{0}\b", alias);
                query = Regex.Replace(query, replacePattern, tableName, RegexOptions.IgnoreCase);
            }

            return query;
        }
    }
}
