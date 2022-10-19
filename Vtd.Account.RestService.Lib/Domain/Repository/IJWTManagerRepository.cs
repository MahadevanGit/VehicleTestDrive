using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vtd.Account.RestService.Lib.Models;

namespace Vtd.Account.RestService.Lib.Domain.Repository
{
    public interface IJWTManagerRepository
    {
        Token Authenticate(User user);
    }
}
