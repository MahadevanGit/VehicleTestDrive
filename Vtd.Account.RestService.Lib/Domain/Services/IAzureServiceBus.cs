using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vtd.Account.RestService.Lib.Models;

namespace Vtd.Account.RestService.Lib.Domain.Services
{
    public interface IAzureServiceBus
    {
        Task SendMessagesAsync(User user);
        Task<List<User>> RecieveMessagesAsync();
    }
}
