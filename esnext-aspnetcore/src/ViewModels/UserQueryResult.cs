using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ViewModels
{
    public class UserQueryResult
    {
        public List<String> ColumnHeaders { get; set; }
        public List<List<String>> Data { get; set; }
    }
}
