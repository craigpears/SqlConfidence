using Common.DataSources.Interfaces;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DataSources.Repositories
{
    public class DataSourceRepository : IDataSourceRepository
    {
        public DataSource Get(int Id)
        {
            return new SqlConfidenceContext().DataSources.Find(Id);
        }

        public List<DataSource> GetAll()
        {
            return new SqlConfidenceContext()
                .DataSources
                .ToList();
        }

        public void Save(DataSource DataSource)
        {
            // Does it already exist?
            var exists = DataSource.DataSourceId != 0;
            var context = new SqlConfidenceContext();

            // Stop it saving any related objects
            DataSource.Exercises = null;

            if (!exists)
            {
                context.DataSources.Add(DataSource);
            }
            else
            {
                context.DataSources.Attach(DataSource);
                context.Entry(DataSource).State = EntityState.Modified;
            }

            context.SaveChanges();
        }
    }
}
