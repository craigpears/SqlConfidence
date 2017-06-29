using DataAccess.Models;
using EntityServices;
using EntityServices.EntityIncludes;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace skeleton
{
    public class IocContainer
    {
        public static Container Container { get; private set; }

        public static void Init(IConfigurationRoot Configuration)
        {
            var optionsBuilder = new DbContextOptionsBuilder<SqlConfidenceContext>().UseSqlServer(Configuration.GetConnectionString("SqlConfidenceContext"));

            IocContainer.Container = new Container(r =>
            {
                r.Scan(x =>
                {
                    x.AssembliesFromApplicationBaseDirectory();
                    x.Assembly("EntityServices");
                    x.WithDefaultConventions();
                    x.ConnectImplementationsToTypesClosing(typeof(IEntityIncludes<>));
                });

                r.For<SqlConfidenceContext>().Use(() => new SqlConfidenceContext(optionsBuilder.Options));
                r.For(typeof(IEntityService<>)).Use(typeof(EntityService<>));
                r.For(typeof(IEntityRepository<>)).Use(typeof(EntityRepository<>));
            });
        }
    }
}
