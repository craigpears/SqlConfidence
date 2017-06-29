using Common;
using Common.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Omu.ValueInjecter;
using System.Timers;
using System.Diagnostics;

namespace UnitTests
{
    [TestClass]
    public class DifferenceEngineTests
    {
        protected DataTable _testData1;
        protected DataTable _testData2;
        protected DataTable _testDataWithSimilarRows;
        protected DataTable _testDataWithIdColumn;

        [TestInitialize]
        public void Init()
        {
            _testData1 = new DataTable();
            _testData1.Columns.Add("Column1");
            _testData1.Rows.Add(new object[] { "somestring" });
            _testData1.Rows.Add(new object[] { "anotherstring" });

            _testData2 = new DataTable();
            _testData2.Columns.Add("ColumnX");
            _testData2.Rows.Add(new object[] { "somestring" });
            _testData2.Rows.Add(new object[] { "anotherstring" });

            _testDataWithSimilarRows = new DataTable();
            _testDataWithSimilarRows.Columns.Add("ColumnX");
            _testDataWithSimilarRows.Columns.Add("ColumnY");
            _testDataWithSimilarRows.Columns.Add("ColumnZ");
            _testDataWithSimilarRows.Columns.Add("ColumnA");
            _testDataWithSimilarRows.Rows.Add(new object[] { "somestring", "anotherstring", 5, 4 });
            _testDataWithSimilarRows.Rows.Add(new object[] { "somestring", "anotherstring", 5, 3 });

            _testDataWithIdColumn = new DataTable();
            _testDataWithIdColumn.Columns.Add("ColumnX");
            _testDataWithIdColumn.Columns.Add("ColumnY");
            _testDataWithIdColumn.Columns.Add("ColumnZ");
            _testDataWithIdColumn.Columns.Add("EX_ID");
            _testDataWithIdColumn.Rows.Add(new object[] { "somestring", "anotherstring", 5, 4 });
            _testDataWithIdColumn.Rows.Add(new object[] { "somestring", "anotherstring", 5, 3 });
        }

        #region GetDifferencesTests

        [TestMethod]
        public void GetDifferences_should_return_no_differences_for_identical_data_set()
        {
            var results = new DifferenceEngine().GetDifferences(_testData1, _testData1);
            Assert.AreEqual(0, results.Differences.Count);
        }

        [TestMethod]
        public void GetDifferences_should_add_column_error_when_columns_are_difference()
        {
            var results = new DifferenceEngine().GetDifferences(_testData1, _testData2);
            Assert.AreEqual(1, results.Differences.Where(x => x.DifferenceType == DataDifferenceType.ColumnMismatch).Count());
        }

        [TestMethod]
        public void GetDifferences_should_add_row_count_error_when_row_count_different()
        {
            DataTable testData1Copy = new DataTable();
            testData1Copy = _testData1.Copy();
            testData1Copy.Rows.Add(new object[] { "blah" });
            var results = new DifferenceEngine().GetDifferences(_testData1, testData1Copy);
            Assert.AreEqual(1, results.Differences.Where(x => x.DifferenceType == DataDifferenceType.RowCountDifferent).Count());
        }

        [TestMethod]
        public void GetDifferences_should_add_mismatch_difference_when_columns_different()
        {
            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();
            dt1.Columns.Add("A");
            dt1.Columns.Add("B");
            dt2.Columns.Add("A");
            dt2.Columns.Add("C");
            var results = new DifferenceEngine().GetDifferences(dt1, dt2);
            Assert.AreEqual(1, results.Differences.Where(x => x.DifferenceType == DataDifferenceType.ColumnMismatch && x.ColumnDifferencePosition == 1).Count());
        }

        [TestMethod]
        public void GetDifferences_should_add_difference_when_column_count_different()
        {
            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();
            dt1.Columns.Add("A");
            dt1.Columns.Add("B");
            dt2.Columns.Add("A");
            var results = new DifferenceEngine().GetDifferences(dt1, dt2);
            Assert.AreEqual(1, results.Differences.Where(x => x.DifferenceType == DataDifferenceType.ColumnCountDifferent).Count());
        }

        [TestMethod]
        public void GetDifferences_should_add_difference_when_rows_are_missing()
        {
            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();
            dt1.Columns.Add("A");
            dt2.Columns.Add("A");
            dt1.Rows.Add(new object[] { "a" });
            var results = new DifferenceEngine().GetDifferences(dt1, dt2);
            Assert.AreEqual(1, results.Differences.Where(x => x.DifferenceType == DataDifferenceType.NotFound).Count());
        }

        [TestMethod]
        public void GetDifferences_should_add_difference_when_rows_in_wrong_order()
        {
            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();
            dt1.Columns.Add("A");
            dt2.Columns.Add("A");
            dt1.Rows.Add(new object[] { "a" });
            dt1.Rows.Add(new object[] { "b" });
            dt2.Rows.Add(new object[] { "b" });
            dt2.Rows.Add(new object[] { "a" });
            var results = new DifferenceEngine().GetDifferences(dt1, dt2);
            Assert.AreEqual(2, results.Differences.Where(x => x.DifferenceType == DataDifferenceType.WrongOrder).Count());
        }

        [TestMethod]
        public void GetDifferences_should_only_return_one_difference_when_stop_at_first_difference_is_specified()
        {
            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();
            dt1.Columns.Add("A");
            dt2.Columns.Add("A");
            dt1.Rows.Add(new object[] { "a" });
            dt1.Rows.Add(new object[] { "b" });
            dt2.Rows.Add(new object[] { "b" });
            dt2.Rows.Add(new object[] { "a" });
            var results = new DifferenceEngine(@StopAtFirstDifference: true).GetDifferences(dt1, dt2);
            Assert.AreEqual(1, results.Differences.Count());
        }

        [TestMethod]
        public void GetDifferences_should_add_difference_when_rows_similar()
        {
            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();
            dt1.Columns.Add("A");
            dt1.Columns.Add("B");
            dt2.Columns.Add("A");
            dt2.Columns.Add("B");
            dt1.Rows.Add(new object[] { "a", 2 });
            dt1.Rows.Add(new object[] { "b", 2 });
            dt2.Rows.Add(new object[] { "b", 1 });
            dt2.Rows.Add(new object[] { "a", 1 });
            var results = new DifferenceEngine(@LookForSimilarRows: true).GetDifferences(dt1, dt2);
            Assert.AreEqual(2, results.Differences.Where(x => x.DifferenceType == DataDifferenceType.SimilarMatchFound).Count());
        }

        [TestMethod]
        public void GetDifferences_should_not_be_slow()
        {
            ISourceDataAccess da = SourceDataAccessFactory.CreateDataAccess(SourceDatabaseType.TSQL);
            DataTable dt1 = da.GetDataTable("select exq.exq_description, count(exqa.exqa_id) 'times_answered' from src_exercises_questions exq  left join src_exercises_questions_answered exqa on exq.exq_id = exqa.exq_id  group by exq.exq_id, exq.exq_description");
            DataTable dt2 = da.GetDataTable("select exq.exq_description, count(*) 'times_answered' from src_exercises_questions exq  left join src_exercises_questions_answered exqa on exq.exq_id = exqa.exq_id  group by exq.exq_id, exq.exq_description");

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            var results = new DifferenceEngine().GetDifferences(dt1, dt2);

            stopwatch.Stop();

            Assert.IsTrue(stopwatch.ElapsedMilliseconds < 200);
        }

        [TestMethod]
        public void GetDifferences_should_not_be_slow_on_large_datasets()
        {
            ISourceDataAccess da = SourceDataAccessFactory.CreateDataAccess(SourceDatabaseType.TSQL);
            DataTable dt1 = da.GetDataTable("select * FROM SRC_EMARKETING_EMAIL ORDER BY EmailId ASC");
            DataTable dt2 = da.GetDataTable("select * FROM SRC_EMARKETING_EMAIL ORDER BY EmailId DESC");

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            var results = new DifferenceEngine().GetDifferences(dt1, dt2);

            stopwatch.Stop();

            Assert.IsTrue(stopwatch.ElapsedMilliseconds < 200);
        }

        #endregion

        #region DataRowsSimilarTests

        [TestMethod]
        public void DataRowsSimilar_should_identify_identical_rows_as_similar()
        {
            var result = new DifferenceEngine().DataRowsSimilar(_testData1.Rows[0], _testData1.Rows[0]);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void DataRowsSimilar_should_identify_rows_with_similar_values_as_similar()
        {
            var result = new DifferenceEngine().DataRowsSimilar(_testDataWithSimilarRows.Rows[0], _testDataWithSimilarRows.Rows[1]);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void DataRowsSimilar_should_never_identify_rows_as_similar_if_they_have_different_id_columns()
        {
            List<int> columnPositionsSimilar;
            var result = new DifferenceEngine().DataRowsSimilar(_testDataWithIdColumn.Rows[0], _testDataWithIdColumn.Rows[1]);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void DataRowsSimilar_should_not_identify_different_numbers_as_similar()
        {
            List<int> columnPositionsSimilar;
            DataTable dt = new DataTable();
            dt.Columns.Add("A");
            dt.Rows.Add(new object[] { 33 });
            dt.Rows.Add(new object[] { 3 });
            var result = new DifferenceEngine().DataRowsSimilar(dt.Rows[0], dt.Rows[1]);

            Assert.IsFalse(result);
        }

        #endregion

        #region IsSimilarNumberTests

        [TestMethod]
        public void IsSimilarNumber_should_return_true_for_identical_numbers()
        {
            var result = new DifferenceEngine().AreSimilarNumbers(1, 1);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsSimilarNumber_should_return_false_for_strings()
        {
            var result = new DifferenceEngine().AreSimilarNumbers("w", "w");

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsSimilarNumber_should_return_true_for_similar_numbers_1()
        {
            var result = new DifferenceEngine().AreSimilarNumbers(21, 25);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsSimilarNumber_should_return_true_for_similar_numbers_2()
        {
            var result = new DifferenceEngine().AreSimilarNumbers(56.5, 57);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsSimilarNumber_should_return_true_for_similar_numbers_3()
        {
            var result = new DifferenceEngine().AreSimilarNumbers(1, 2);

            Assert.IsTrue(result);
        }

        #endregion
    }
}
