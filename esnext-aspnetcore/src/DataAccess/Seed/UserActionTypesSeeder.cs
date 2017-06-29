using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Seed
{
    public class UserActionTypesSeeder
    {
        public void Seed(SqlConfidenceContext context)
        {
            if (context.UserActionTypes.Any()) return;
            context.UserActionTypes.Add(
                new UserActionType
                {
                    Description = "Execute Query"
                }
            );

            context.UserActionTypes.Add(
                new UserActionType
                {
                    Description = "Check Answer"
                }
            );

            context.SaveChanges();
        }
    }
}
