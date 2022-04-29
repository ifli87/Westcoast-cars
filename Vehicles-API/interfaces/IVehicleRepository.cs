using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vehicles_API.Models;
using Vehicles_API.ViewModels;

namespace Vehicles_API.interfaces
{
    public interface IVehicleRepository 
    {
        // vi har skapat signaturer för våra metoder, ett regelverk  vill man utnytja detta interfacet måste man skriva kod för alla metoder, allt eller inget. 
        public Task<List<VehicleViewModel>> ListAllVehiclesAsync();
        public Task <VehicleViewModel?> GetVehicleAsync(int id);
        public Task <VehicleViewModel?> GetVehicleAsync(string regNumber);
        public Task AddVehicleAsync(Vehicle model);
        public void DeleteVehicle(int id);
        public void UpdateVehicle (int id, Vehicle model);
        public Task <bool> SaveAllAsync();
    }
}