using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ViewModels;
using Services;
using Microsoft.Extensions.Configuration;

namespace skeleton.api
{
    public class IncomingData
    {
        public string sqlquery {get;set;}
    }

    [Produces("application/json")]
    [Route("api/userquery")]
    public class UserQueryController: Controller
    {
        [HttpPost]
        public UserQueryResult Get([FromBody]IncomingData data)
        {
            var service = new UserQueries(Startup.Configuration.GetConnectionString("SqlConfidenceData"));
            var result = service.Executequery(data.sqlquery);

            return result;
        }
    }
}
