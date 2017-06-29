using Common.Models;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DataSources.Interfaces
{
    public interface IDataSourceRepository
    {
        DataSource Get(int Id);
        List<DataSource> GetAll();
        void Save(DataSource DataSource);
    }
}
