using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DataAccess.Models;
using EntityServices;

namespace skeleton.api
{
    [Produces("application/json")]
    [Route("api/exercise")]
    public class ExerciseController : Controller
    {
        IEntityService<MultipleChoiceExercise> _multipleChoiceService;
        IEntityService<QueryBuilderExercise> _queryBuilderService;
        IEntityService<QueryExercise> _queryService;
        IEntityService<UnitTestedExercise> _unitTestedService;

        public ExerciseController()
        {
            _multipleChoiceService = IocContainer.Container.GetInstance<IEntityService<MultipleChoiceExercise>>();
            _queryBuilderService = IocContainer.Container.GetInstance<IEntityService<QueryBuilderExercise>>();
            _queryService = IocContainer.Container.GetInstance<IEntityService<QueryExercise>>();
            _unitTestedService = IocContainer.Container.GetInstance<IEntityService<UnitTestedExercise>>();
        }

        public List<Exercise> Get()
        {
            var data = new List<Exercise>();
            data.AddRange(_multipleChoiceService.GetAll());
            data.AddRange(_queryBuilderService.GetAll());
            data.AddRange(_queryService.GetAll());
            data.AddRange(_unitTestedService.GetAll());
            return data;
        }
    }
}