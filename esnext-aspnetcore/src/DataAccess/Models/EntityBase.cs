using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models
{
    public abstract class EntityBase
    {
        public DateTime CreatedDate { get; set; }
        [MaxLength(50)]
        public String CreatedBy { get; set; }
        public Nullable<DateTime> UpdatedDate { get; set; }
        [MaxLength(50)]
        public String UpdatedBy { get; set; }
        public Nullable<DateTime> DeletedDate { get; set; }
        [MaxLength(50)]
        public String DeletedBy { get; set; }
        [DefaultValue("false")]
        public Boolean Deleted { get; set; }
    }
}
