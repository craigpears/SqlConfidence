﻿using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Seed
{
    public class UsersSeeder
    {
        public void Seed(SqlConfidenceContext context)
        {
            context.Users.AddOrUpdate(
                x => x.Email,
                new User
                {
                    FirstName = "Admin",
                    LastName = "User",
                    Email = "admin@sqlconfidence.com",
                    CreatedDate = DateTime.Now,
                    CreatedBy = "SeedScript",
                    IsAdmin = true,
                    Password = "5f4dcc3b5aa765d61d8327deb882cf99" //Password
                });

            context.SaveChanges();
        }
    }
}
