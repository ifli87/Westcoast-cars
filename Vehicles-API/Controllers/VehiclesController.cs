using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vehicles_API.Data;
using Vehicles_API.interfaces;
using Vehicles_API.Models;
using Vehicles_API.ViewModels;

namespace Vehicles_API.Controllers
{
    [ApiController]
    [Route("api/v1/vehicles")]
    public class VehiclesController : ControllerBase
    {
        private readonly VehicleContext _context;
        private readonly IVehicleRepository _vehicleRepo;
        private readonly IMapper _mapper;
    
        public VehiclesController(VehicleContext context, IVehicleRepository vehicleRepo,IMapper mapper)
        {
            _mapper = mapper;
            _vehicleRepo = vehicleRepo;
            _context = context;

        }

        [HttpGet()]
       public async Task <ActionResult<List<VehicleViewModel>>> ListVehicles()
       {
        //    var response = await _context.Vehicles.ToListAsync();
        //    var response = await _vehicleRepo.ListAllVehiclesAsync();
        //    var vehicleList = new List<VehicleViewModel>();
        //    foreach (var vehicle in response)
        //    {
        //           vehicleList.Add(
        //   new VehicleViewModel
        //   {
        //     VehicleId = vehicle.Id,
        //     RegNo = vehicle.RegNo,
        //     VehicleName = string.Concat(vehicle.Maker, " ", vehicle.Model),
        //     ModelYear = vehicle.ModelYear,
        //     Mileage = vehicle.Mileage
        //   }
        // );
        //    }

        //    var vehicleList = _mapper.Map<List<VehicleViewModel>>(response); 
        // var vehicleList = await _vehicleRepo.ListAllVehiclesAsync();
           return Ok(await _vehicleRepo.ListAllVehiclesAsync());
       }  


         [HttpGet("{id}")]
        public async Task<ActionResult<VehicleViewModel>> GetVehicleById(int id )
       {
           var response = await _vehicleRepo.GetVehicleAsync(id);
           
           if (response is null)
           return NotFound ($"Vi kunde inte hittta någon bil med ditt id{id}");


        //     var vehicle = new VehicleViewModel
        //   {
        //     VehicleId = response.Id,
        //     RegNo = response.RegNo,
        //     VehicleName = string.Concat(response.Maker, " ", response.Model),
        //     ModelYear = response.ModelYear,
        //     Mileage = response.Mileage
        //   };

        
         return Ok(response);
       }  

       [HttpGet("byregno/{regNo}")]
       public async Task<ActionResult<Vehicle>> GetVehicleByRegNo (string regNo)
       {
           var response = await _vehicleRepo.GetVehicleAsync(regNo);
           if (response is null)
           return NotFound($"vi kunde inte hitta någon bil med registreringsnr {regNo} ");

           return Ok(response);
       }



       [HttpPost]
       public  async  Task <ActionResult<Vehicle>> AddVehicle (PostVehicleViewModel vehicle)
       {
        //    var vehicleToAdd = new Vehicle
        //    {
        //        RegNo = vehicle.RegNo,
        //        Maker = vehicle.Maker,
        //        Model = vehicle.Model,
        //        ModelYear = vehicle.ModelYear,
        //        Mileage = vehicle.Mileage
        //    };
            var vehicleToAdd = _mapper.Map<Vehicle>(vehicle);
           await _context.Vehicles.AddAsync(vehicleToAdd);
           await _context.SaveChangesAsync();
          return StatusCode(201, vehicleToAdd);
       }



           [HttpPut("{id}")]
       public async Task<ActionResult> UpdateVehicle (int id, Vehicle model)
       {
           var response = await _context.Vehicles.FindAsync(id);
            if (response is null) return NotFound($"vi kunde inte hitta någon bil med id {id} so skulle tas bort ");

            response.RegNo = model.RegNo;
            response.Maker = model.Maker;
            response.Model = model.Model;
            response.ModelYear = model.ModelYear;
            response.Mileage = model.Mileage;

            _context.Vehicles.Update(response);
            await _context.SaveChangesAsync();

           return NoContent();
       }



          [HttpDelete("{id}")]
       public async Task<ActionResult> DeleteVehicle (int id)
       {

            _vehicleRepo.DeleteVehicle(id);

                                   
          if( await _vehicleRepo.SaveAllAsync()){

           return NoContent();
          }
          return StatusCode(500, "Hoppsan något gick fel");
       }
    }
}