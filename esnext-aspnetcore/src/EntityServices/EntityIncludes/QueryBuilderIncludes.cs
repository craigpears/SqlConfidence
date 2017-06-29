using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntityServices.EntityIncludes
{
    public class QueryBuilderIncludes : IEntityIncludes<QueryBuilderExercise>
    {
        public IQueryable<QueryBuilderExercise> AddIncludes(DbSet<QueryBuilderExercise> dbSet)
        {
            return dbSet.Include(x => x.ExerciseQuestions).AsQueryable();
        }
    }
}