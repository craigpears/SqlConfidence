using StructureMap;
using StructureMap.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class IocContainer
    {
        public static Container Container { get; private set; }

        public static void Init()
        {
            IocContainer.Container = new Container(r =>
            {
                r.Scan(x =>
                {
                    x.AssembliesFromApplicationBaseDirectory();
                    x.WithDefaultConventions();
                });
            });
        }
    }
}
