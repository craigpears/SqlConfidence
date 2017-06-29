using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Models
{
    public class BaseModel
    {
        public DateTime CreatedDate { get; set; }
        public String CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public String UpdatedBy { get; set; }
    }
}
