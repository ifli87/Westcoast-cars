using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Vehicles_API.Data;
using Vehicles_API.interfaces;
using Vehicles_API.Models;
using Vehicles_API.ViewModels;
using Vehicles_API.ViewModels.Manufacturer;

namespace Vehicles_API.Repositories
{
    public class ManufacturerRepository : IManufacturerRepository
    {
        private readonly IMapper _mapper;

        private readonly VehicleContext _context;
        public ManufacturerRepository(VehicleContext context, IMapper mapper )
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task AddManufacturerAsync(PostManufacturerViewModel model)
        {
            var maker = _mapper.Map<Manufacturer>(model);
            await _context.Manufacturers.AddAsync(maker);
            throw new NotImplementedException();
        }

        public async Task<List<ManufacturerViewModel>> ListManufacturerAsync()
        {
           return await _context.Manufacturers.ProjectTo<ManufacturerViewModel>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task<List<ManufacturersWithVehiclesViewModel>> ListManufacturersVehicles()
        {
            return await _context.Manufacturers.Include(c => c.Vehicles)
            .Select(m => new ManufacturersWithVehiclesViewModel{
            ManufacturerId = m.Id,
            Name = m.Name,
            Vehicles = m.Vehicles.Select(v => new VehicleViewModel{
                VehicleId = v.Id,
                RegNo = v.RegNo,
                VehicleName = string.Concat(v.Manufacturer.Name, "" , v.Model),
                ModelYear = v.ModelYear,
                Mileage = v.Mileage
            }).ToList()
            }).ToListAsync();
        }

        public async Task<ManufacturersWithVehiclesViewModel?> ListManufacturersVehicles(int id)
        {
              return await _context.Manufacturers.Where(c => c.Id == id).Include(c => c.Vehicles)
            .Select(m => new ManufacturersWithVehiclesViewModel{
            ManufacturerId = m.Id,
            Name = m.Name,
            Vehicles = m.Vehicles.Select(v => new VehicleViewModel{
                VehicleId = v.Id,
                RegNo = v.RegNo,
                VehicleName = string.Concat(v.Manufacturer.Name!, "" , v.Model!),
                ModelYear = v.ModelYear,
                Mileage = v.Mileage
            }).ToList()
            }).SingleOrDefaultAsync();
        }

        public async Task<bool> SaveALLAsync()
        {
           return await _context.SaveChangesAsync() > 0;
        }
    }
}