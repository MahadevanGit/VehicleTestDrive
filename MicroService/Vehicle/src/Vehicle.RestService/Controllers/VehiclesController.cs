using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vehicle.RestService.Lib.Domain.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Vehicle.RestService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class VehiclesController : ControllerBase
    {
        private readonly IVehicleService _vehicleService;

        public VehiclesController(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        // GET: api/<VehiclesController>
        [HttpGet]
        public IActionResult GetAsync()
        {
            var userId = HttpContext.User?.Identity?.Name;
            var data = _vehicleService.Get();
            if(data != null)
            {
                return Ok(data);
            }
            else
            {
                return Unauthorized();
            }
        }

        // GET api/<VehiclesController>/5
        [HttpGet("{id}")]
        public async Task<Lib.Models.Vehicle> GetAsync(int id)
        {
            return await _vehicleService.GetById(id);
        }

        // POST api/<VehiclesController>
        [HttpPost]
        public async Task PostAsync([FromBody] Vehicle.RestService.Lib.Models.Vehicle value)
        {
           await _vehicleService.Add(value);
        }

        // PUT api/<VehiclesController>/5
        [HttpPut("{id}")]
        public async Task PutAsync(int id, [FromBody] Vehicle.RestService.Lib.Models.Vehicle value)
        {
            await _vehicleService.Update(id, value);
        }

        // DELETE api/<VehiclesController>/5
        [HttpDelete("{id}")]
        public async Task DeleteAsync(int id)
        {
            await _vehicleService.Delete(id);
        }
    }
}
