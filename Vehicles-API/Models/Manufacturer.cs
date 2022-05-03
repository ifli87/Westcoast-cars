using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vehicles_API.Models
{
    public class Manufacturer
    {
        public int Id { get; set; } 
        public string? Name { get; set; }
        public ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
    }
}