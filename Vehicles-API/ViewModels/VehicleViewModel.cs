using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Vehicles_API.ViewModels
{
    public class VehicleViewModel
    {
        public int VehicleId { get; set; }
        
         public string? RegNo { get; set; }
        public string? VehicleName { get; set; }
       
        public int ModelYear { get; set; }
        public int Mileage { get; set; }   
    }
}