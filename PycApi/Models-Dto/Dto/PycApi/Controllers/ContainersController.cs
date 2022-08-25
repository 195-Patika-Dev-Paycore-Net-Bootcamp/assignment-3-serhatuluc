using Microsoft.AspNetCore.Mvc;
using PycApi.Context;
using PycApi.Context.VehicleSession;
using PycApi.Model;
using PycApi.Models_Dto.Dto;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;


namespace PycApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ContainersContoller : ControllerBase
    {
        //Both session of container and vehicle are needed
        private readonly VehicleIMapperSession v_session;
        private readonly ContainerIMapperSession c_session;
        public ContainersContoller(ContainerIMapperSession session, VehicleIMapperSession vsession)
        {
            this.v_session = vsession;
            this.c_session = session;
        }

        [HttpGet]
        public List<Containers> Get()
        {
            List<Containers> result = c_session.Containers.ToList();
            return result;
        }


        [HttpGet("{id}")]
        public Containers Get(int id)
        {
            Containers result = c_session.Containers.Where(x => x.Id == id).FirstOrDefault();
            return result;
        }

       


        [HttpPost]
        public ActionResult<Vehicle> Post([FromBody] ContainerCreateDto newcontainer)
        {
            //Dto is used here to prevent id to be asked 

            Vehicle vehicle = v_session.Vehicles.Where(x => x.Id == newcontainer.vehicle).FirstOrDefault();
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
            try
            {
                c_session.BeginTransaction();
                c_session.Save(container);
                c_session.Commit();
            }
            catch (Exception ex)
            {
                c_session.Rollback();
                Log.Error(ex, "Container Insert Error");
            }
            finally
            {
                c_session.CloseTransaction();
            }
            return Ok();
        }

        [HttpPut]
        public ActionResult<Vehicle> Put([FromBody] ContainerUpdateDto request)
        {
            Containers container = c_session.Containers.Where(x => x.Id == request.Id).FirstOrDefault();
            if (container == null)
            {
                return NotFound();
            }

            try
            {
                c_session.BeginTransaction();

                container.containerName = request.containerName;
                container.latitude = request.latitude;
                container.longitude = request.longitude;


                c_session.Update(container);

                c_session.Commit();
            }
            catch (Exception ex)
            {
                c_session.Rollback();
                Log.Error(ex, "Containers Insert Error");
            }
            finally
            {
                c_session.CloseTransaction();
            }


            return Ok();
        }


        [HttpDelete("{id}")]
        public ActionResult<Containers> Delete(int id)
        {
            Containers container = c_session.Containers.Where(x => x.Id == id).FirstOrDefault();
            if (container == null)
            {
                return NotFound();
            }

            try
            {
                c_session.BeginTransaction();
                c_session.Delete(container);
                c_session.Commit();
            }
            catch (Exception ex)
            {
                c_session.Rollback();
                Log.Error(ex, "Book Insert Error");
            }
            finally
            {
                c_session.CloseTransaction();
            }

            return Ok();
        }


    }
}
