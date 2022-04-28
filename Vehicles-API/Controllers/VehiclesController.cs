using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vehicles_API.Data;
using Vehicles_API.Models;
using Vehicles_API.ViewModels;

namespace Vehicles_API.Controllers
{
    [ApiController]
    [Route("api/v1/vehicles")]
    public class VehiclesController : ControllerBase
    {
        private readonly VehicleContext _context;
        public VehiclesController(VehicleContext context)
        {
            _context = context;

        }

        [HttpGet()]
       public async Task <ActionResult<List<VehicleViewModel>>> ListVehicles()
       {
           var response = await _context.Vehicles.ToListAsync();
           var vehicleList = new List<VehicleViewModel>();
           foreach (var vehicle in response)
           {
                  vehicleList.Add(
          new VehicleViewModel
          {
            VehicleId = vehicle.Id,
            RegNo = vehicle.RegNo,
            VehicleName = string.Concat(vehicle.Maker, " ", vehicle.Model),
            ModelYear = vehicle.ModelYear,
            Mileage = vehicle.Mileage
          }
        );
           }
           return Ok(vehicleList);
       }  


         [HttpGet("{id}")]
        public async Task<ActionResult<Vehicle>> GetVehicleById(int id )
       {
           
         var response = await _context.Vehicles.FindAsync(id);
         return Ok(response);
       }  

       [HttpGet("byregno/{regNo}")]
       public async Task<ActionResult<Vehicle>> GetVehicleByRegNo (string regNo)
       {
           var response = await _context.Vehicles.SingleOrDefaultAsync(c => c.RegNo!.ToLower() == regNo.ToLower());
           if (response is null)
           return NotFound($"vi kunde inte hitta någon bil med registreringsnr {regNo} ");

           return Ok(response);
       }



       [HttpPost]
       public  async  Task <ActionResult<Vehicle>> AddVehicle (PostVehicleViewModel vehicle)
       {
           var vehicleToAdd = new Vehicle
           {
               RegNo = vehicle.RegNo,
               Maker = vehicle.Maker,
               Model = vehicle.Model,
               ModelYear = vehicle.ModelYear,
               Mileage = vehicle.Mileage
           };
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

           var response = await _context.Vehicles.FindAsync(id);

           if (response is null) return NotFound($"vi kunde inte hitta någon bil med id {id} so skulle tas bort ");
           _context.Vehicles.Remove(response);
           await _context.SaveChangesAsync();
           return NoContent();
       }
    }
}