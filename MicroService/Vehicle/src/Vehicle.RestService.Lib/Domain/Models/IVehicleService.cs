using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vehicle.RestService.Lib.Domain.Models
{
    public interface IVehicleService
    {
        IEnumerable<Vehicle.RestService.Lib.Models.Vehicle> Get();
        Task<Vehicle.RestService.Lib.Models.Vehicle> GetById(int Id);
        Task Add(Vehicle.RestService.Lib.Models.Vehicle vehicle);
        Task Update(int Id,Vehicle.RestService.Lib.Models.Vehicle vehicle);
        Task Delete(int Id);

    }
}
