using Customer.RestService.Lib.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Customer.RestService.Lib.Domain
{
    public class CustomerDBContext : DbContext
    {
        //If we add dbcontext in startup.cs(Vehicle.RestService project) then no need to add below OnConfiguring method.
        //protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
        //{
        //    Data Source=C830LX065472536;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False
        //    dbContextOptionsBuilder.UseSqlServer(@"Server=C830LX065472536;Database=Vehicle;");
        //}
        public DbSet<Customer.RestService.Lib.Models.Customer> Customers { get; set; }
        public DbSet<CustomerVehicle> CustomerVehicles { get; set; }
        public CustomerDBContext(DbContextOptions<CustomerDBContext> options) : base(options)
        {
        }
        protected CustomerDBContext(DbContextOptions options) : base(options)
        {
        }
    }

    public class CustomerDBContextReadOnly : CustomerDBContext
    {
        public CustomerDBContextReadOnly(DbContextOptions<CustomerDBContextReadOnly> options) : base(options)
        {
        }
    }
}
