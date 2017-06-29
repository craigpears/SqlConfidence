using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    public class ExerciseModel : BaseModel
    {
        public int ExerciseId { get; set; }
        public int DataSourceId { get; set; }
        public string Name { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        public DataSourceModel DataSource { get; set; }
        public List<QuestionModel> Questions { get; set; }
        public Boolean Published { get; set; }
        public DateTime PublishedDate { get; set; }
        public String SectionName { get; set; }
        public int Order { get; set; }
        public Boolean ShowQueryBuilder { get; set; }
        public Guid ExerciseIdGuid { get; set; }
    }
}
