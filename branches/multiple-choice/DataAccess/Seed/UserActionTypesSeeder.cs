using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Seed
{
    public class UserActionTypesSeeder
    {
        public void Seed(SqlConfidenceContext context)
        {
            context.UserActionTypes.AddOrUpdate(
                x => x.Id,
                new UserActionType
                {
                    Id = 0,
                    Description = "Execute Query"
                },
                new UserActionType
                {
                    Id = 1,
                    Description = "Check Answer"
                }
            );

            context.SaveChanges();
        }
    }
}
