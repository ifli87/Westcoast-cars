using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vehicles_API.Models;
using Vehicles_API.ViewModels.Manufacturer;

namespace Vehicles_API.interfaces
{
    public interface IManufacturerRepository
    {
        public Task AddManufacturerAsync(PostManufacturerViewModel model);

        public Task<List<ManufacturerViewModel>> ListManufacturerAsync();
        public Task<List<ManufacturersWithVehiclesViewModel>> ListManufacturersVehicles();
        public Task<ManufacturersWithVehiclesViewModel?> ListManufacturersVehicles(int id);
        public Task <bool> SaveALLAsync();

    }
}