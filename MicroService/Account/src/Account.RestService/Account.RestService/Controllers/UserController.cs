using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vtd.Account.RestService.Lib.Domain.Repository;
using Vtd.Account.RestService.Lib.Domain.Services;
using Vtd.Account.RestService.Lib.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Account.RestService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IJWTManagerRepository _iJWTManagerRepository;
        private readonly IUserService _userService;
        private readonly IAzureServiceBus _azureServiceBus;
        public UserController(
            IJWTManagerRepository iJWTManagerRepository, 
            IUserService userService,
            IAzureServiceBus azureServiceBus)
        {
            _iJWTManagerRepository = iJWTManagerRepository;
            _userService = userService;
            _azureServiceBus = azureServiceBus;
        }
        // GET: api/<UserController>
        [HttpGet]
        public IEnumerable<User> Get()
        {
            return _userService.Get();
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("Login")]
        public IActionResult Login([FromBody] User user)
        {
            var token = _iJWTManagerRepository.Authenticate(user);
            if (token == null)
            {
                return Unauthorized();
            }
            return Ok(token);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> AddUserAsync([FromBody] User user)
        {
            var _user = await _userService.Add(user);
            
            if (_user != null)
            {
                if(_user.RoleId == 2) // role id 2 for customer. so add into azure message service bus to store in to customer db.
                await _azureServiceBus.SendMessagesAsync(_user);
                return Ok("User added successfully.");
            }
            else
                return Ok("User already exist in the system.");
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<UserController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
