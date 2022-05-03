using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Vehicles_API.ViewModels
{
    public class PostVehicleViewModel
    {
        [Required (ErrorMessage = "Registreringsnummer är obligatoriskt")]
        public string? RegNo { get; set; }
        public string? Maker { get; set; }
        public string? Model { get; set; }
        public int ModelYear { get; set; }
        public int Mileage { get; set; }    
        
        
    }
}