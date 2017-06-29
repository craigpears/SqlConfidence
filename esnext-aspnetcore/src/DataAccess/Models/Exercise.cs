using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace DataAccess.Models
{
    [DataContract]
    public abstract class Exercise:EntityBase
    {
        public Exercise() { }
        [Key]
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int DataSourceId { get; set; }
        [DataMember]
        public int SectionId { get; set; }

        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Discriminator { get; set; }
        [DataMember]
        public string Summary { get; set; }
        public Nullable<Boolean> Published { get; set; }
        public Nullable<DateTime> PublishedDate { get; set; }
        public string PublishedBy { get; set; }
        [DataMember]
        public int Order { get; set; }
        [DataMember]
        public virtual DataSource DataSource { get; set; }
        [DataMember]
        public virtual Section Section { get; set; }
    }
}
