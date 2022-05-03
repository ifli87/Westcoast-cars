using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Vehicles_API.interfaces;
using Vehicles_API.ViewModels.Manufacturer;

namespace Vehicles_API.Controllers
{
    [ApiController]
    [Route("api/v1/manufacturers")]
    public class ManufacturerController : ControllerBase
    {
        private readonly IManufacturerRepository _repo;
       
        public ManufacturerController(IManufacturerRepository repo)
        {
            _repo = repo;
            

        }

        [HttpGet()]
        public async Task<ActionResult> ListAllManufacturors()
        {
            return Ok();
        }

         [HttpGet("{id}")]
        public async Task<ActionResult> GetManufacturerById()
        {
            return Ok();
        }
        [HttpGet("vehicles")]
        public async Task <ActionResult> ListManufacturersAndVehicles()
        {
            return Ok (await _repo.ListManufacturersVehicles());
        }
         [HttpGet("{id}/vehicles")]
        public async Task <ActionResult> ListManufacturersVehicles(int id)
        {
            var result = await _repo.ListManufacturersVehicles(id);
         if (result is null)
         {
             return NotFound($"Vi kunde inte hitta någon tillverkare med id {id}");
         }
             return Ok(result);
        }
         [HttpPost()]
        public async Task<ActionResult> AddManufaturer(PostManufacturerViewModel model)
        {
            await _repo.AddManufacturerAsync(model);
            if(await _repo.SaveALLAsync())
            {
                return StatusCode(201);
            }
            return StatusCode(500, "Det gick fel när vi skulle spara tillverkaren");
        }

           [HttpPut("{id}")]
        public async Task<ActionResult> UpdateManufacturer(int id)
        {
            return NoContent();
        }
           [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteManufacturer()
        {
            return Ok();
        }
    }
}
       