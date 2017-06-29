using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Common.Models;
using Common.QueryBuilder;
using System.Collections.Generic;
using DataAccess.Models;

namespace UnitTests
{
    [TestClass]
    public class TSQLQueryBuilderTests
    {
        QueryExercise _testExercise;
        ExerciseQuestion _testQuestion;
        TSQLQueryBuilder _testClass;


        [TestInitialize]
        public void Init()
        {
            _testExercise = new QueryExercise()
            {
                DataSource = new DataSource()
                {
                    DataSourceTables = new List<DataSourceTable>() 
                    {
                        new DataSourceTable() 
                        {
                            TableAlias = "test", TableName="test_test" 
                        } 
                    }
                }
            };

            _testQuestion = new MultipleChoiceQuestion();
            _testClass = new TSQLQueryBuilder();
        }

        [TestMethod]
        public void should_replace_whole_table_names_only()
        {
            var result = _testClass.BuildQuery("test_table", _testExercise, _testQuestion);
            Assert.IsTrue(result == "test_table");
        }

        [TestMethod]
        public void should_replace_table_aliases_with_their_table_names()
        {
            var result = _testClass.BuildQuery("test", _testExercise, _testQuestion);
            Assert.IsTrue(result == "test_test");
        }

        [TestMethod]
        public void should_replace_table_aliases_when_prefixed_with_dbo_dot()
        {
            var result = _testClass.BuildQuery("dbo.test", _testExercise, _testQuestion);
            Assert.IsTrue(result == "dbo.test_test");
        }
    }
}
