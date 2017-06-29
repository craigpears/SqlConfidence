using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntityServices.EntityIncludes
{
    public class MultipleChoiceIncludes : IEntityIncludes<MultipleChoiceExercise>
    {
        public IQueryable<MultipleChoiceExercise> AddIncludes(DbSet<MultipleChoiceExercise> dbSet)
        {
            return dbSet
                .Include(x => x.ExerciseQuestions).ThenInclude(x => x.Options)
                .Include(x => x.ExerciseQuestions).ThenInclude(x => x.DataQueries)
                .AsQueryable();
        }
    }
}