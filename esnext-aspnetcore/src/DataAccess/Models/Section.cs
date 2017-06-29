using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class Section:EntityBase
    {
        public Section()
        {

        }

        [Key]
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Description { get; set; }

        public virtual List<Exercise> Exercises { get; set; }
    }
}
