using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntityServices.EntityIncludes
{
    public class UnitTestIncludes : IEntityIncludes<UnitTestedExercise>
    {
        public IQueryable<UnitTestedExercise> AddIncludes(DbSet<UnitTestedExercise> dbSet)
        {
            return dbSet.Include(x => x.ExerciseQuestions).AsQueryable();
        }
    }
}