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
    
        private readonly IVehicleRepository _vehicleRepo;
        private readonly IMapper _mapper;
    
        public VehiclesController( IVehicleRepository vehicleRepo,IMapper mapper)
        {
            _mapper = mapper;
            _vehicleRepo = vehicleRepo;
        

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

       [HttpGet("bymake/{maker}")]
       public async Task<ActionResult<List<VehicleViewModel>>> GetVehicleByMaker(string maker)
       {
        //  return Ok(await _vehicleRepo.GetVehicleByMaker(maker));
        return Ok();
       }



       [HttpPost]
       public async Task <ActionResult> AddVehicle (PostVehicleViewModel model)
       {
         try
         {
               if(await _vehicleRepo.GetVehicleAsync(model.RegNo!.ToLower())is not null)
           {
               return BadRequest ($"Registreringsnummer {model.RegNo} finns redan i systemet ");
           }

           await _vehicleRepo.AddVehicleAsync(model);

            if (await _vehicleRepo.SaveAllAsync())
            {

          return StatusCode(201);
            }
            return StatusCode(500, "Det gick innte att spara fordonet!");
         }
         catch (Exception ex)
         {
           
          return StatusCode(500, ex.Message);
         }

       
       }



           [HttpPut("{id}")]
       public async Task<ActionResult> UpdateVehicle (int id, PostVehicleViewModel model)
         {
      try
      {
        await _vehicleRepo.UpdateVehicle(id, model);

        if (await _vehicleRepo.SaveAllAsync())
        {
          return NoContent(); //Status kod 204...
        }

        return StatusCode(500, "Ett fel inträffade när fordonet skulle uppdateras");

      }
      catch (Exception ex)
      {
        return StatusCode(500, ex.Message);
      }
    }

        [HttpPatch("{{id}")]
        public async Task<ActionResult> UpdateVehicle (int id, PatchVehicleViewModel model)
        {
          try
          {
            await _vehicleRepo.UpdateVehicle(id, model);
            if(await _vehicleRepo.SaveAllAsync())
            {
              return NoContent();
            }
            return StatusCode (500,"Ett fel inträffade när fordonnet skulle uppdateras");
          }
          catch (Exception ex)
          {
            return StatusCode (500, ex.Message);
          }
        }



       [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteVehicle(int id)
    {
      await _vehicleRepo.DeleteVehicle(id);

      if (await _vehicleRepo.SaveAllAsync())
      {
        return NoContent(); // Status kod 204
      }

      return StatusCode(500, "Hoppsan något gick fel");

    }
    }
}