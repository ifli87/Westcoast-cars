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
        //  public Task <List<VehicleViewModel>> GetVehicleByMaker(string maker);
        public Task AddVehicleAsync(PostVehicleViewModel model);
        public Task DeleteVehicle(int id);
        public Task UpdateVehicle (int id, PostVehicleViewModel model);
         public Task UpdateVehicle (int id, PatchVehicleViewModel model);
        public Task <bool> SaveAllAsync();
    }
}