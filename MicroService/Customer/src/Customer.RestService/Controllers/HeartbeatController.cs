using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vtd.Customer.RestService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HeartbeatController : ControllerBase
    {
        // GET: api/<HeartbeatController>
        /// <summary>
        /// To find Vtd.Customer.RestService.Api is running?
        /// </summary>
        /// <returns> Vtd.Customer.RestService.Api is Running. </returns>
        /// <remarks>
        /// Sample request api/Heartbeat
        /// </remarks>
        [HttpGet]


        public string Get()
        {
            return "Vtd.Customer.RestService.Api is Running.";
        }
    }
}
