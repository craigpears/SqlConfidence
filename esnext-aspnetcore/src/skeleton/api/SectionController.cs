using DataAccess.Models;
using EntityServices;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace skeleton.api
{
    [Produces("application/json")]
    [Route("api/section")]
    public class SectionController : Controller
    {
        IEntityService<Section> _sectionService;

        public SectionController()
        {
            _sectionService = IocContainer.Container.GetInstance<IEntityService<Section>>();
        }

        public List<Section> Get()
        {
            var data = _sectionService.GetAll();
            return data;
        }
    }
}
