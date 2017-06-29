using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Seed
{
    public class DataSourceSeeder
    {
        public void Seed(SqlConfidenceContext context)
        {
            if (context.DataSources.Any()) return;

            context.DataSources.Add(
                new DataSource
                {
                    Name = "Staff",
                    CreatedBy = "SeedScript",
                    CreatedDate = DateTime.Now,
                    DataSourceTables = new List<DataSourceTable>()
                    {
                        new DataSourceTable()
                        {
                            TableName = "SRC_ORGANISATIONS_AND_STAFF",
                            CreatedBy = "SeedScript",
                            CreatedDate = DateTime.Now
                        }
                    }
                });
            context.SaveChanges();
        }
    }
}
