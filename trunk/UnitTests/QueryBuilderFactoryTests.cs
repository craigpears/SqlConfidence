using Common.Enums;
using Common.QueryBuilder;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
    [TestClass]
    public class QueryBuilderFactoryTests
    {
        [TestMethod]
        public void QueryBuilderFactoryTests_should_return_tsql_query_builder_for_tsql()
        {
            IQueryBuilder result = QueryBuilderFactory.CreateQueryBuilder(SourceDatabaseType.TSQL);
            Assert.IsTrue(result.GetType() == typeof(TSQLQueryBuilder));
        }
    }
}
