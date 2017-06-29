using Common.Models;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DataSources.Interfaces
{
    public interface IDataSourceTableService
    {
        DataSourceTable Get(int Id);
        List<DataSourceTable> GetAll();
        List<DataSourceTable> GetByDataSourceId(int Id);
        void Save(DataSourceTable DataSourceTable, UserModel user);
    }
}
