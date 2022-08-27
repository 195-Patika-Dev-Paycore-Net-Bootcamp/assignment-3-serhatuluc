using Microsoft.AspNetCore.Mvc;
using PycApi.Context;
using PycApi.Model;
using PycApi.Models_Dto.Dto;
using System.Collections.Generic;
using System.Linq;

namespace PycApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class VehicleContoller : ControllerBase
    {
        private readonly IVehicleRepository v_session;
        private readonly IContainerRepository c_session;
        public VehicleContoller(IContainerRepository csession, IVehicleRepository vsession)
        {
            this.v_session = vsession;
            this.c_session = csession;
        }

        [HttpGet]
        public IEnumerable<Vehicle> Get()
        {
            return v_session.GetAll();
        }


        [HttpGet("{id}")]
        public ActionResult<Vehicle> Get(int id)
        {
            Vehicle vehicle = v_session.GetById(id);
            if (vehicle is null)
            {
                return NotFound("Vehicle not found");
            }
            return vehicle;
        }


       

        [HttpPost]
        public void Post([FromBody] VehicleCreateDto newvehicle)
        {
            //Dto is used here to hide id from client
            Vehicle vehicle = new Vehicle()
            {
                name = newvehicle.name,
                plate = newvehicle.plate
            };
            v_session.Add(vehicle);
        }

        [HttpPut]
        public ActionResult<Vehicle> Put([FromBody] Vehicle request)
        {
            Vehicle vehicle = v_session.GetById(request.Id);
            if (vehicle == null)
            {
                return NotFound();
            }

            //Attributes are modified
            vehicle.name = request.name;
            vehicle.plate = request.plate;

            //Vehicle is updated
            v_session.Update(vehicle);
            return Ok();
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            //Vehicle is fetched
            Vehicle vehicle = v_session.GetById(id);

            if (vehicle == null)
            {
                return NotFound();
            }

            //List of container which has the vehicle id
            List<Containers> listOfContainer = c_session.GetAll().Where(x => x.vehicle == id).ToList();
           //Containers are deleted
            c_session.DeleteAll(listOfContainer);

            //Vehicle is deleted
            v_session.Delete(vehicle);

            return Ok();
        }


    }
}
