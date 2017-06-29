using Common.DataSources.Interfaces;
using Common.Exercises.Interfaces;
using Common.Models;
using Common.Questions.Interfaces;
using DataAccess.Enums;
using DataAccess.Models;
using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DataSources.Services
{
    public class DataSourceTableService : IDataSourceTableService
    {
        protected IDataSourceTableRepository _dataSourceTableRepository;
        protected List<TableModel> _tableModels;

        public DataSourceTableService(
            IDataSourceTableRepository DataSourceTableRepository)
        {
            _dataSourceTableRepository = DataSourceTableRepository;
            var sourceDA = SourceDataAccessFactory.CreateDataAccess(Enums.SourceDatabaseType.TSQL);
            _tableModels = sourceDA.ListAllTables();
        }

        public DataSourceTable Get(int Id)
        {
            var dataSourceTable = _dataSourceTableRepository.Get(Id);
            dataSourceTable = this.Create(dataSourceTable);
            return dataSourceTable;
        }

        public List<DataSourceTable> GetAll()
        {
            var dataSourceTables = _dataSourceTableRepository.GetAll();
            dataSourceTables = this.Create(dataSourceTables);
            return dataSourceTables;
        }

        public List<DataSourceTable> GetByDataSourceId(int Id)
        {
            var dataSourceTables = _dataSourceTableRepository.GetByDataSourceId(Id);
            dataSourceTables = this.Create(dataSourceTables);
            return dataSourceTables;
        }

        public void Save(DataSourceTable DataSourceTable, UserModel user)
        {
            if (String.IsNullOrEmpty(DataSourceTable.CreatedBy))
            {
                DataSourceTable.CreatedBy = user.Email;
                DataSourceTable.CreatedDate = DateTime.Now;
                DataSourceTable.DataSourceTableIdGuid = Guid.NewGuid();
            }
            else
            {
                DataSourceTable.UpdatedBy = user.Email;
                DataSourceTable.UpdatedDate = DateTime.Now;
            }

            _dataSourceTableRepository.Save(DataSourceTable);
        }

        protected List<DataSourceTable> Create(List<DataSourceTable> DataSourceTables)
        {
            foreach (var dataSourceTable in DataSourceTables)
            {
                this.Create(dataSourceTable);
            }
            return DataSourceTables;
        }

        protected DataSourceTable Create(DataSourceTable DataSourceTable)
        {
            this.PopulateColumnsForTable(DataSourceTable);
            return DataSourceTable;
        }

        protected void PopulateColumnsForTable(DataSourceTable DataSourceTable)
        {
            var tableModel = _tableModels.Single(x => x.TableName == DataSourceTable.TableName);
            DataSourceTable.Columns = SerializerHelper.Serialize(tableModel.Columns);
        }
    }
}
