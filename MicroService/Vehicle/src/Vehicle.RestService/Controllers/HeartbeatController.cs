using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Vehicle.RestService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HeartbeatController : ControllerBase
    {
        // GET: api/<HeartbeatController>
        [HttpGet]
        public string Get()
        {
            return "Vtd.Vehicle.RestService.Api is Running.";
        }
    }
}
