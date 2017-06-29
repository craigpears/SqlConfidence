using Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    public class SourceDataAccessFactory
    {
        public static ISourceDataAccess CreateDataAccess(SourceDatabaseType Type)
        {
            switch (Type)
            {
                case SourceDatabaseType.TSQL:
                    ISourceDataAccess sda = new TSQLDataAccess();
                    return sda;
                default:
                    throw new Exception("Unrecognised source data type");
            }
        }
    }
}
