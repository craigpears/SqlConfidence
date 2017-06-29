using Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.QueryBuilder
{
    public class QueryBuilderFactory
    {
        public static IQueryBuilder CreateQueryBuilder(SourceDatabaseType Type)
        {
            switch (Type)
            {
                case SourceDatabaseType.TSQL:
                    return new TSQLQueryBuilder();
                default:
                    throw new Exception("Error creating query builder - unrecognised type");
            }
        }
    }
}
