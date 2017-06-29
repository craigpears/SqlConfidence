using Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Models
{
    public class DataSyncItemModel : BaseModel
    {
        public Guid Id { get; set; }
        public String Name { get; set; }
        public String ViewLink { get; set; }
        public ObjectType ObjectType { get; set; }
        public InstanceSourceType SourceType { get; set; }
    }
}
