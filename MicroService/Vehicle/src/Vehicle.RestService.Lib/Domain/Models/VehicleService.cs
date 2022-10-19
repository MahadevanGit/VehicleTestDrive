using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicle.RestService.Lib.Domain.DBContext;

namespace Vehicle.RestService.Lib.Domain.Models
{
    public class VehicleService : IVehicleService
    {
        private readonly VehicleDBContext _vehicleDbContext;

        public VehicleService(VehicleDBContext vehicleDBContext)
        {
            _vehicleDbContext = vehicleDBContext;
        }
        public async Task Add(Lib.Models.Vehicle vehicle)
        {
            await _vehicleDbContext.Vehicles.AddAsync(vehicle);
            await _vehicleDbContext.SaveChangesAsync();
        }

        public async Task Delete(int Id)
        {
            var vehicle = await _vehicleDbContext.Vehicles.FindAsync(Id);
            _vehicleDbContext.Vehicles.Remove(vehicle);
            await _vehicleDbContext.SaveChangesAsync();
        }

        public IEnumerable<Lib.Models.Vehicle> Get()
        {
            var vehicles = _vehicleDbContext.Vehicles.ToList();
            return vehicles;
        }

        public async Task<Lib.Models.Vehicle> GetById(int Id)
        {
            var vehicle = await _vehicleDbContext.Vehicles.FindAsync(Id);
            return vehicle;
        }

        public async Task Update(int Id, Lib.Models.Vehicle vehicle)
        {
            var vehicleObj = await _vehicleDbContext.Vehicles.FindAsync(Id);
            vehicleObj.Name = vehicle.Name;
            vehicleObj.Price = vehicle.Price;
            vehicleObj.MaxSpeedByKM = vehicle.MaxSpeedByKM;
            vehicleObj.BodyColor = vehicle.BodyColor;
            vehicleObj.Weight = vehicle.Weight;
            vehicleObj.Length = vehicle.Length;
            vehicleObj.Width = vehicle.Width;
            vehicleObj.Make = vehicle.Make;
            vehicleObj.Model = vehicle.Model;
            _vehicleDbContext.Update(vehicleObj);
            await _vehicleDbContext.SaveChangesAsync();
        }
    }
}
