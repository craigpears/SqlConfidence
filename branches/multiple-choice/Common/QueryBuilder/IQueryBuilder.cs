using Common.Models;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.QueryBuilder
{
    public interface IQueryBuilder
    {
        String BuildQuery(String BaseCommand, Exercise Exercise, ExerciseQuestion Question);
    }
}
