using Common;
using Common.BusinessLogicServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
    [TestClass]
    public class DataTableModelHelperTests
    {
        [TestMethod]
        public void rows_and_columns_should_be_in_the_same_order()
        {
            var dt = new DataTable();
            dt.Columns.Add("ColumnX");
            dt.Columns.Add("ColumnY");
            dt.Rows.Add(new object[] { "somestring", "anotherstring" });
            var model = DataTableModelHelper.DataTableToModel(dt);
            var firstRow = model.Rows[0].ToArray();
            Assert.AreEqual("somestring", firstRow[0].Value);
            Assert.AreEqual("anotherstring", firstRow[1].Value);
            Assert.AreEqual("ColumnX", model.Columns[0].Name);
            Assert.AreEqual("ColumnY", model.Columns[1].Name);
        }
    }
}
