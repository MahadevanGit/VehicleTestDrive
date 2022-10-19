using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Vehicle.RestService.Lib.Domain.DBContext
{
    public class VehicleDBContext : DbContext
    {
        //If we add dbcontext in startup.cs(Vehicle.RestService proj) then no need to add here.
        //protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
        //{
        //    Data Source=C830LX065472536;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False
        //    dbContextOptionsBuilder.UseSqlServer(@"Server=C830LX065472536;Database=Vehicle;");
        //}
        public DbSet<Vehicle.RestService.Lib.Models.Vehicle> Vehicles { get; set; } 
        public VehicleDBContext(DbContextOptions<VehicleDBContext> options) : base(options)
        {
        }
        protected VehicleDBContext(DbContextOptions options) : base(options)
        {
        }
    }

    public class VehicleDBContextReadOnly : VehicleDBContext
    {
        public VehicleDBContextReadOnly(DbContextOptions<VehicleDBContextReadOnly> options) : base(options)
        {
        }
    }
}
