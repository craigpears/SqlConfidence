using Common;
using Common.EntityServices.Exercises.MultipleChoice;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SqlConfidence.Api
{
    public class MultipleChoiceController : ApiController
    {
        IMultipleChoiceExerciseService _service;
        
        public MultipleChoiceController() {
            _service = IocContainer.Container.GetInstance<IMultipleChoiceExerciseService>();
        }

        // GET api/multiplechoice/5
        public MultipleChoiceExercise Get(int id)
        {
            return _service.Get(id);
        }

        // POST api/multiplechoice
        public void Post([FromBody]MultipleChoiceExercise value)
        {
            throw new NotImplementedException();
        }

        // PUT api/multiplechoice/5
        public void Put(int id, [FromBody]MultipleChoiceExercise value)
        {
            throw new NotImplementedException();
        }

        // DELETE api/multiplechoice/5
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
