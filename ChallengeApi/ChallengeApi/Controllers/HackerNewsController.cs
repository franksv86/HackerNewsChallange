using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CallengeApi.BLL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CallengeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HackerNewsController : ControllerBase
    {
        HackerNewEntity _entity;
        public HackerNewsController(HackerNewEntity entity) {
            this._entity = entity;
        }

        [HttpGet("getpage")]
        public ActionResult GetPage([FromQuery] int take, [FromQuery]string search, CancellationToken cancellationToken)
        {

            var ids = _entity.GetLastIds().Result;

            if (take == 0)
                take = ids.Count;

            var page = _entity.GetPage(ids, take, search, cancellationToken);

            return Ok(page);
        }


    }
}