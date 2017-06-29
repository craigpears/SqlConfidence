using Common.DataSources.Interfaces;
using Common.Exercises.Interfaces;
using Common.Models;
using Common.Questions.Interfaces;
using DataAccess.Enums;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DataSources.Services
{
    public class DataSourceService : IDataSourceService
    {
        protected IDataSourceRepository _dataSourceRepository;
        protected IDataSourceTableService _dataSourceTableService;

        public DataSourceService(
            IDataSourceRepository DataSourceRepository,
            IDataSourceTableService DataSourceTableService)
        {
            _dataSourceRepository = DataSourceRepository;
            _dataSourceTableService = DataSourceTableService;
        }

        public DataSource Get(int Id)
        {
            var dataSource = _dataSourceRepository.Get(Id);
            dataSource = this.Create(dataSource);
            return dataSource;
        }

        public List<DataSource> GetAll()
        {
            var dataSources = _dataSourceRepository.GetAll();
            dataSources = this.Create(dataSources);
            return dataSources;
        }
        
        public void Save(DataSource DataSource, UserModel user)
        {
            if (String.IsNullOrEmpty(DataSource.CreatedBy))
            {
                DataSource.CreatedBy = user.Email;
                DataSource.CreatedDate = DateTime.Now;
                DataSource.DataSourceIdGuid = Guid.NewGuid();
            }
            else
            {
                DataSource.UpdatedBy = user.Email;
                DataSource.UpdatedDate = DateTime.Now;
            }

            _dataSourceRepository.Save(DataSource);
        }

        protected List<DataSource> Create(List<DataSource> DataSources)
        {
            foreach (var dataSource in DataSources)
            {
                this.Create(dataSource);
            }
            return DataSources;
        }

        protected DataSource Create(DataSource DataSource)
        {
            DataSource.DataSourceTables = _dataSourceTableService.GetByDataSourceId(DataSource.DataSourceId);

            // Link the child objects to the data source
            foreach (var item in DataSource.DataSourceTables)
                item.DataSource = DataSource;

            return DataSource;
        }
    }
}
