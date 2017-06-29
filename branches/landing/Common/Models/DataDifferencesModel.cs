using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Models
{
    public class DataDifferencesModel
    {        
        public int UserDataColumnCount { get; set; }
        public int AnswerDataColumnCount { get; set; }
        public int UserDataRowCount { get; set; }
        public int AnswerDataRowCount { get; set; }
        public List<DataDifferenceModel> Differences { get; set; }

        public DataDifferencesModel()
        {
            Differences = new List<DataDifferenceModel>();
        }
    }
}
