using Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.QueryBuilder
{
    public interface IQueryBuilder
    {
        String BuildQuery(String BaseCommand, ExerciseModel Exercise, QuestionModel Question);
    }
}
