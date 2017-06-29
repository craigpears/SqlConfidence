using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntityServices.EntityIncludes
{
    public class QueryIncludes : IEntityIncludes<QueryExercise>
    {
        public IQueryable<QueryExercise> AddIncludes(DbSet<QueryExercise> dbSet)
        {
            return dbSet.Include(x => x.ExerciseQuestions).AsQueryable();
        }
    }
}