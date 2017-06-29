using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Models.Context
{
    public class SqlConfidenceContextFactory : IDbContextFactory<SqlConfidenceContext>
    {
        public SqlConfidenceContext Create(DbContextFactoryOptions options)
        {
            return new SqlConfidenceContext(options);
        }
    }
}
