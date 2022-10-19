using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vtd.Account.RestService.Lib.Models;

namespace Vtd.Account.RestService.Lib.Domain.Services
{
    public interface IUserService
    {
        Task<User> Add(User user);
        IEnumerable<User> Get();
        Task<User> GetById(int id);
    }
}
