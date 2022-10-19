using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Customer.RestService.Lib.Models
{
    public class CustomerVehicle
    {
        [Key]
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int VehicleId { get; set; }
    }
}
