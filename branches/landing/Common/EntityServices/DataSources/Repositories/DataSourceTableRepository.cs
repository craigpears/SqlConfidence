using Common.DataSources.Interfaces;
using Common.Exercises.Interfaces;
using Common.Questions.Interfaces;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DataSources.Repositories
{
    public class DataSourceTableRepository : IDataSourceTableRepository
    {
        public DataSourceTable Get(int Id)
        {
            return new SqlConfidenceContext().DataSourceTables.Find(Id);
        }

        public List<DataSourceTable> GetAll()
        {
            return new SqlConfidenceContext()
                .DataSourceTables
                .ToList();
        }

        public List<DataSourceTable> GetByDataSourceId(int Id)
        {
            return new SqlConfidenceContext()
                .DataSourceTables
                .Where(x => x.DataSourceId == Id)
                .ToList();
        }

        public void Save(DataSourceTable DataSourceTable)
        {
            // Does it already exist?
            var exists = DataSourceTable.DataSourceTableId != 0;
            var context = new SqlConfidenceContext();

            // Stop it saving any related objects
            DataSourceTable.DataSource = null;

            if (!exists)
            {
                context.DataSourceTables.Add(DataSourceTable);
            }
            else
            {
                context.DataSourceTables.Attach(DataSourceTable);
                context.Entry(DataSourceTable).State = EntityState.Modified;
            }

            context.SaveChanges();
        }
    }
}
