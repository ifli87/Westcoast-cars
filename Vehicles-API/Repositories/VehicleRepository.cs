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

namespace Vehicles_API.Repositories
{

    // metodkroppar som vi definierar i interfacet
    public class VehicleRepository : IVehicleRepository
    {
        private readonly VehicleContext _context;
        private readonly IMapper _mapper;
        public VehicleRepository(VehicleContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
            
        }

        public async Task AddVehicleAsync(PostVehicleViewModel model)
        {
            // steg 1 omvandla postvehicleviewmodel till vehicle typen
            var maker = await _context.Manufacturers.Include(c => c.Vehicles).Where(c =>c.Name!.ToLower() == model.Maker!.
            ToLower()).SingleOrDefaultAsync();

            if(maker is null)
            {
                throw new Exception($"Tyvärr vi har inte tillverkaren {model.Maker} i systenet");
            }
            var vehicleToAdd = _mapper.Map<Vehicle>(model);
            vehicleToAdd.Manufacturer = maker;
            await _context.Vehicles.AddAsync(vehicleToAdd);
          
            
            throw new NotImplementedException();
        }

        public async Task  DeleteVehicle(int id)
        {
             var response = await _context.Vehicles.FindAsync(id);

            if(response is not null) {

         _context.Vehicles.Remove(response);
            }
        }

        public async Task<VehicleViewModel?> GetVehicleAsync(int id)
        {
            // return await _context.Vehicles.Where(c => c.Id == id)
            // .Select(vehicle => new VehicleViewModel{
            //     VehicleId = vehicle.Id,
            //     RegNo = vehicle.RegNo,
            //     VehicleName = string.Concat(vehicle.Maker, " " , vehicle.Model),
            //     ModelYear = vehicle.ModelYear,
            //     Mileage = vehicle.Mileage
            //      }).SingleOrDefaultAsync();
            return await _context.Vehicles.Where(c =>c.Id == id)
            .ProjectTo<VehicleViewModel>(_mapper.ConfigurationProvider)
            .SingleOrDefaultAsync();
        }

        public async Task<VehicleViewModel?> GetVehicleAsync(string regNumber)
        {
            //       return await _context.Vehicles.Where(c => c.RegNo == regNumber)
            // .Select(vehicle => new VehicleViewModel{
            //     VehicleId = vehicle.Id,
            //     RegNo = vehicle.RegNo,
            //     VehicleName = string.Concat(vehicle.Maker, " " , vehicle.Model),
            //     ModelYear = vehicle.ModelYear,
            //     Mileage = vehicle.Mileage
            //      }).SingleOrDefaultAsync();
            return await _context.Vehicles.Where(c =>c.RegNo == regNumber)
            .ProjectTo<VehicleViewModel>(_mapper.ConfigurationProvider)
            .SingleOrDefaultAsync();
        }

        // public async Task<List<VehicleViewModel>> GetVehicleByMaker(string maker)
        // {
        //      return await _context.Vehicles
        //     .Where(c => c.Maker!.ToLower() == maker.ToLower())
        //     .ProjectTo<VehicleViewModel>(_mapper.ConfigurationProvider)
        //     .ToListAsync();
        // }

        public async Task<List<VehicleViewModel>> ListAllVehiclesAsync()
        {
           return await _context.Vehicles.ProjectTo<VehicleViewModel>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task<bool> SaveAllAsync()
        {
           return await _context.SaveChangesAsync() > 0;
        }

        

        public async Task UpdateVehicle(int id, PostVehicleViewModel model)
        {
            var vehicle = await _context.Vehicles.FindAsync(id);

            if (vehicle is null)
            {
                throw new Exception($"Vi kunde inte hitta något fordon med id: {id}");
            }
            vehicle.RegNo = model.RegNo;
            // vehicle.Maker = model.Maker;
            vehicle.Model = model.Model;
            vehicle.ModelYear = model.ModelYear;
            vehicle.Mileage = model.Mileage;
            
            _context.Vehicles.Update(vehicle);
            
            
        }

        public async Task UpdateVehicle(int id, PatchVehicleViewModel model)
        {
             var vehicle = await _context.Vehicles.FindAsync(id);

            if (vehicle is null)
            {
                throw new Exception($"Vi kunde inte hitta något fordon med id: {id}");
            }
            vehicle.ModelYear = model.ModelYear;
            vehicle.Mileage = model.Mileage;

            _context.Vehicles.Update(vehicle);
        }
    }
}