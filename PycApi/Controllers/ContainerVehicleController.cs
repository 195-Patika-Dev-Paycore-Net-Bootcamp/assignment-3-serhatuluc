using Microsoft.AspNetCore.Mvc;
using PycApi.Context;
using PycApi.Context.VehicleSession;
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
        private readonly VehicleIMapperSession v_session;
        private readonly ContainerIMapperSession c_session;
        public ContainerVehicleController(ContainerIMapperSession session, VehicleIMapperSession vsession)
        {
            this.v_session = vsession;
            this.c_session = session;
        }
        [HttpGet("GetContainersByVehicle/{id}")]
        public List<Containers> GetContainers(int id)
        {
            List<Containers> result = c_session.Containers.Where(x => x.vehicle == id).ToList();
            return result;
        }

        [HttpGet("CreateSetOfContainers/{id,N}")]
        public IEnumerable<IEnumerable<Containers>> Get(int id, int N)
        {
            IEnumerable<Containers> listOfContainers = c_session.Containers.Where(x => x.vehicle == id).ToList();

            return listOfContainers.Split(N);
        }
    }
}
