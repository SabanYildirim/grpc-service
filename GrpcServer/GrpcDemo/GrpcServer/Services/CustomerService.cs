using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using GrpcServer;
using Microsoft.Extensions.Logging;

namespace GrpcServer.Services
{
    public class CustomerService : Customers.CustomersBase
    {
        private readonly ILogger<CustomerService> _logger;
        public CustomerService(ILogger<CustomerService> logger)
        {
            _logger = logger;
        }

        public override Task<CustomerModel> GetCustomerInfo(CustomerLoopModel request, ServerCallContext context)
        {
            CustomerModel output = new CustomerModel();

            if (request.UserId == 1)
            {
                output.FirstName = "ahmet";
                output.LastName = "yilmaz";
            }

            if (request.UserId == 2)
            {
                output.FirstName = "veli";
                output.LastName = "örnek";
            }

            else
            {
                output.FirstName = "ali";
                output.LastName = "yıldırım";
            }

            return Task.FromResult(output);
        }


        

        public override async Task GetNewCustomers(
            NewCustomerRequest request,
            IServerStreamWriter<CustomerModel> responseStream, 
            ServerCallContext context)
        {
            List<CustomerModel> customers = new List<CustomerModel>
            {
                new CustomerModel
                {
                     FirstName = "saban",
                     LastName = "yıldırım",
                     EmailAddress = "sbn@gmail.com",
                     Age = 29,
                     IsAlive = true
                },
                new CustomerModel
                {
                     FirstName = "mehmet",
                     LastName = "yılmaz",
                     EmailAddress = "mehmet@gmail.com",
                     Age = 30,
                     IsAlive = true
                },
                new CustomerModel
                {
                     FirstName = "cristian",
                     LastName = "ronaldo",
                     EmailAddress = "ronalda@gmail.com",
                     Age = 35,
                     IsAlive = false
                }
            };

            foreach (var cust in customers)
            {
                await responseStream.WriteAsync(cust);
            }
        
        }
    }
}
