using Azure.Messaging.ServiceBus;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vtd.Account.RestService.Lib.Infra.Bootstrap;
using Vtd.Account.RestService.Lib.Models;

namespace Vtd.Account.RestService.Lib.Domain.Services
{
    public class AzureServiceBus : IAzureServiceBus
    {
        private readonly IConfiguration _configuration;

        // connection string to your Service Bus namespace
        private readonly string _connectionString;
        // name of your Service Bus queue
        private readonly string _queueName;
        public AzureServiceBus(IHostingEnvironment env)
        {
            _configuration = new ConfigurationBuilder()
                .AddCombinedConfig(env) //Add this using Vehicle.RestService.Lib.Infra.Bootstrap for AddCombinedConfig(env)
                .AddEnvironmentVariables()
                .Build();
            _connectionString = _configuration["ServiceBus:VtdAccountSBConn"];
            _queueName = _configuration["ServiceBus:VtdAccountSBQueueName"];
        }

        public async Task SendMessagesAsync(User user)
        {
            await using var client = new ServiceBusClient(_connectionString);
            ServiceBusSender serviceBusSender = client.CreateSender(_queueName);
            ServiceBusMessage message = new ServiceBusMessage(JsonConvert.SerializeObject(user));
            await serviceBusSender.SendMessageAsync(message);
        }

        public async Task<List<User>> RecieveMessagesAsync()
        {
            var userList = new List<User>();
            await using var client = new ServiceBusClient(_connectionString);

            // create a receiver that we can use to receive the message
            ServiceBusReceiver receiver = client.CreateReceiver(_queueName);

            // the received message is a different type as it contains some service set properties
            IReadOnlyList<ServiceBusReceivedMessage> receivedMessage = await receiver.ReceiveMessagesAsync(10);

            foreach (ServiceBusReceivedMessage item in receivedMessage)
            {
                // get the message body as a string
                string body = item.Body.ToString();

                var user = JsonConvert.DeserializeObject<User>(body);
                userList.Add(user);

                // to remove the message from azure service bus queue
                await receiver.CompleteMessageAsync(item);
            }

            return userList;
        }
    }
}
