using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Vtd.Account.RestService.Lib.Domain.Services;
using Vtd.Account.RestService.Lib.Infra.Bootstrap;
using Vtd.Account.RestService.Lib.Models;

namespace Vtd.Account.RestService.Lib.Domain.Repository
{
    public class JWTManagerRepository : IJWTManagerRepository
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;

        public JWTManagerRepository(IHostingEnvironment env, IUserService userService)
        {
            _userService = userService;
            _configuration = new ConfigurationBuilder()
                .AddCombinedConfig(env) //Add this using Vehicle.RestService.Lib.Infra.Bootstrap for AddCombinedConfig(env)
                .AddEnvironmentVariables()
                .Build();
        }
        public Token Authenticate(User user)
        {
            var users = _userService.Get();
            var config = _configuration;
            //get singapore time
            var siTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Singapore Standard Time");
            DateTime siTime = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.Local, siTimeZone);

            //If user not found in db then return token as null

            if (!users.Any(u => u.Name == user.Name && u.Password == user.Password))
            {
                return null;
            }
            //else we generate token
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.UTF8.GetBytes(_configuration["JWT:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(
                    new Claim[]
                    {
                        new Claim(ClaimTypes.Name, user.Name)
                    }),
                Expires = siTime.AddMinutes(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return new Token { CurrentToken = tokenHandler.WriteToken(token) };
        }
    }
}

