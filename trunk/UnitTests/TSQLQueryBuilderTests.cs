using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Common.Models;
using Common.QueryBuilder;
using System.Collections.Generic;

namespace UnitTests
{
    [TestClass]
    public class TSQLQueryBuilderTests
    {
        ExerciseModel _testExercise;
        QuestionModel _testQuestion;
        TSQLQueryBuilder _testClass;


        [TestInitialize]
        public void Init()
        {
            _testExercise = new ExerciseModel()
            {
                DataSource = new DataSourceModel()
                {
                    Tables = new List<TableModel>() 
                    {
                        new TableModel() 
                        {
                            Alias = "test", TableName="test_test" 
                        } 
                    }
                }
            };

            _testQuestion = new QuestionModel();
            _testClass = new TSQLQueryBuilder();
        }

        [TestMethod]
        public void should_replace_whole_table_names_only()
        {
            Assert.IsTrue(false);
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
