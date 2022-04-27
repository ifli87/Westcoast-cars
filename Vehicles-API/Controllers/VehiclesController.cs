using Microsoft.AspNetCore.Mvc;

namespace Vehicles_API.Controllers
{
    [ApiController]
    [Route("api/v1/vehicles")]
    public class VehiclesController : ControllerBase
    {
        [HttpGet()]
       public ActionResult ListVehicles()
       {
           return StatusCode(200, "{'message': 'Det funkar'}");
       }  
         [HttpGet("{id}")]
        public ActionResult GetVehicleById(int id )
       {
           
           return StatusCode(200, "{'message': 'Det funkar också'}");
       }  

       [HttpPost]
       public ActionResult AddVehicle ()
       {
          return Ok();
       }

           [HttpPut("{id}")]
       public ActionResult UpdateVehicle (int id)
       {
           return NoContent();
       }

          [HttpDelete("{id}")]
       public ActionResult DeleteVehicle (int id)
       {
           return NoContent();
       }
    }
}