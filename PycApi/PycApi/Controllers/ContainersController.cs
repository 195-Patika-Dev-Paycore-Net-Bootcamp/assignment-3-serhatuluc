using Microsoft.AspNetCore.Mvc;
using PycApi.Context;
using PycApi.Model;
using PycApi.Models_Dto.Dto;
using System.Collections.Generic;



namespace PycApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ContainersContoller : ControllerBase
    {
        //Both session of container and vehicle are needed
        private readonly IVehicleRepository v_session;
        private readonly IContainerRepository c_session;
        public ContainersContoller(IContainerRepository csession, IVehicleRepository vsession)
        {
            this.v_session = vsession;
            this.c_session = csession;
        }

        [HttpGet]
        public IEnumerable<Containers> Get()
        {
            return c_session.GetAll();
        }


        [HttpGet("{id}")]
        public ActionResult<Containers> Get(int id)
        {
            Containers container = c_session.GetById(id);
            if (container is null)
            {
                return NotFound("Container not found");
            }
            return container;
        }

       


        [HttpPost]
        public ActionResult<Containers> Post([FromBody] ContainerCreateDto newcontainer)
        {
            //Dto is used here to prevent id to be asked 

            Vehicle vehicle = v_session.GetById(newcontainer.vehicle);
            if (vehicle == null)
            {
                return NotFound("Vehicle not found");
            }
            Containers container = new Containers()
            {
                //Data from dto is used to create an object of container
                containerName = newcontainer.containerName,
                latitude = newcontainer.latitude,
                longitude = newcontainer.longitude,
                vehicle = newcontainer.vehicle
            };
            c_session.Add(container);
           
            return Ok();
        }

        [HttpPut]
        public ActionResult<Vehicle> Put([FromBody] ContainerUpdateDto request)
        {
            //Another dto is used here to update vehicle
            Containers container = c_session.GetById(request.Id);
            if(container is null)
            {
                return NotFound("Container not found");
            }
            //Conversion of dto is done here.
            container.containerName = request.containerName;
            container.latitude = request.latitude;
            container.longitude = request.longitude;
            c_session.Update(container);
            return Ok();
        }


        [HttpDelete("{id}")]
        public ActionResult<Containers> Delete(int id)
        {
            
            Containers container = c_session.GetById(id);
            if(container is null)
            {
                return NotFound("Container not found");
            }
            //Vehicle is deleted 
            c_session.Delete(container);

            return Ok();
        }


    }
}
