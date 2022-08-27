using Microsoft.AspNetCore.Mvc;
using PycApi.Context;
using PycApi.Extensions;
using PycApi.Model;
using System.Collections.Generic;
using System.Linq;

namespace PycApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ContainerVehicleController:ControllerBase
    {
        private readonly IVehicleRepository v_session;
        private readonly IContainerRepository c_session;
        public ContainerVehicleController(IContainerRepository csession, IVehicleRepository vsession)
        {
            this.v_session = vsession;
            this.c_session = csession;
        }


        [HttpGet("GetContainersByVehicle/{id}")]
        public ActionResult<List<Containers>> GetContainers(int id)
        {
            if(v_session.GetById(id) is null)
            {
                return NotFound("Vehicle not found");
            }
            //List of container which has id of vehicle is listed
            List<Containers> result = c_session.GetAll().Where(x => x.vehicle == id).ToList();
            return result;
        }

        [HttpGet("CreateSetOfContainers/{id,N}")]
        public List<List<Containers>> Get(int id, int N)
        {
            List<Containers> listOfContainers = c_session.GetAll().Where(x => x.vehicle == id).ToList();

            //Extension is used here
            return listOfContainers.Split(N);
        }
    }
}
