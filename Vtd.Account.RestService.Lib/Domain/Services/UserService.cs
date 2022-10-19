using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vtd.Account.RestService.Lib.Domain.DBContext;
using Vtd.Account.RestService.Lib.Models;

namespace Vtd.Account.RestService.Lib.Domain.Services
{
    public class UserService : IUserService
    {
        private readonly AccountDBContext _accountDbContext;
        public UserService(AccountDBContext accountDbContext)
        {
            _accountDbContext = accountDbContext;
        }

        public async Task<User> Add(User user)
        {
            var users = _accountDbContext.Users.ToList();
            if (users.Any(u => u.Name.ToLower() == user.Name.ToLower()))
                return null;
            else
            {
                await _accountDbContext.Users.AddAsync(user);
                await _accountDbContext.SaveChangesAsync();
                return user;
            }
        }

        public async Task<User> GetById(int id)
        {
            var user = await _accountDbContext.Users.FindAsync(id);
            return user;
        }
        public IEnumerable<User> Get()
        {
            var users = _accountDbContext.Users.ToList();
            return users;
        }
    }
}
