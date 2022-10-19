using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Account.RestService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HeartbeatController : ControllerBase
    {
        private readonly ILogger<HeartbeatController> _logger;
        public HeartbeatController(ILogger<HeartbeatController> logger)
        {
            _logger = logger;
        }
        // GET: api/<Heartbeat>
        [HttpGet]
        public string Get()
        {
            var logName = "HearBeatController";
            _logger.LogInformation($"Name : {logName} , Information : Vtd.Account.RestService.Api is running.");
            return "Vtd.Account.RestService.Api is running.";
        }
    }
}
