using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class vwExercisesQuestionsUser
    {
        public int EXQ_ID { get; set; }
        public int EX_ID { get; set; }
        public string EXQ_INSTRUCTIONS_TEMPLATE { get; set; }
        public string EXQ_ANSWERS_TEMPLATE { get; set; }
        public string EXQ_DESCRIPTION { get; set; }
        public string EXQ_CREATED_BY { get; set; }
        public System.DateTime EXQ_CREATED_DATE { get; set; }
        public string EXQ_UPDATED_BY { get; set; }
        public Nullable<System.DateTime> EXQ_UPDATED_DATE { get; set; }
        public bool EXQ_DELETED { get; set; }
        public string EXQ_DELETED_BY { get; set; }
        public Nullable<System.DateTime> EXQ_DELETED_DATE { get; set; }
        public string EXQ_HINT { get; set; }
        public Nullable<int> EXQ_ORDER { get; set; }
        public string EXQ_STARTING_SQL { get; set; }
        public System.Guid EXQ_ID_GUID { get; set; }
        public int us_id { get; set; }
        public Nullable<System.DateTime> AnsweredDate { get; set; }
        public Nullable<bool> Answered { get; set; }
    }
}
