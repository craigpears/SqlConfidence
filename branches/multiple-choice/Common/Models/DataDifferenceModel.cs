using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Enums;
using System.Data;

namespace Common.Models
{
    public class DataDifferenceModel
    {
        // Always populated
        public DataDifferenceType DifferenceType { get; set; }
        
        // Populated for Column mismatch type
        public int ColumnDifferencePosition { get; set; }
        public String AnswerQueryColumn { get; set; }
        public String UserQueryColumn { get; set; }

        // Populated for row mismatches
        public int UserQueryPosition { get; set; }
        public DataRow UserQueryRow { get; set; }

        // Populated for WrongOrder
        public int AnswerQueryPosition { get; set; }

        // Populated for similar row found
        public DataRow AnswerQueryRow { get; set; }
        public List<int> ColumnPositionsSimilar { get; set; }
    }
}
