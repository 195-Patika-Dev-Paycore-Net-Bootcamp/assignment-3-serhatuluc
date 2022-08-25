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
    [Route("api/nhb/[controller]")]
    public class VehicleContoller : ControllerBase
    {
        private readonly ContainerIMapperSession c_session;
        private readonly VehicleIMapperSession v_session;
        public VehicleContoller(VehicleIMapperSession session, ContainerIMapperSession csession)
        {
            this.v_session = session;
            this.c_session = csession;
        }

        [HttpGet]
        public List<Vehicle> Get()
        {
            List<Vehicle> result = v_session.Vehicles.ToList();
            return result;
        }


        [HttpGet("{id}")]
        public Vehicle Get(int id)
        {
            Vehicle result = v_session.Vehicles.Where(x => x.Id == id).FirstOrDefault();
            return result;
        }


       

        [HttpPost]
        public void Post([FromBody] VehicleCreateDto newvehicle)
        {
            Vehicle vehicle = new Vehicle()
            {
                name = newvehicle.name,
                plate = newvehicle.plate
            };
            try
            {
                v_session.BeginTransaction();
                v_session.Save(vehicle);
                v_session.Commit();
            }
            catch (Exception ex)
            {
                v_session.Rollback();
                Log.Error(ex, "Vehicle Insert Error");
            }
            finally
            {
                v_session.CloseTransaction();
            }
        }

        [HttpPut]
        public ActionResult<Vehicle> Put([FromBody] Vehicle request)
        {
            Vehicle vehicle = v_session.Vehicles.Where(x => x.Id == request.Id).FirstOrDefault();
            if (vehicle == null)
            {
                return NotFound();
            }

            try
            {
                v_session.BeginTransaction();

                vehicle.name = request.name;
                vehicle.plate = request.plate;


                v_session.Update(vehicle);

                v_session.Commit();
            }
            catch (Exception ex)
            {
                v_session.Rollback();
                Log.Error(ex, "Vehicle Insert Error");
            }
            finally
            {
                v_session.CloseTransaction();
            }


            return Ok();
        }


        [HttpDelete("{id}")]
        public ActionResult<Vehicle> Delete(int id)
        {
            Vehicle vehicle = v_session.Vehicles.Where(x => x.Id == id).FirstOrDefault();

            if (vehicle == null)
            {
                return NotFound();
            }

            try
            {
                v_session.BeginTransaction();
                v_session.Delete(vehicle);
                v_session.Commit();

                List<Containers> listOfContainer = c_session.Containers.Where(x => x.vehicle == vehicle.Id).ToList();

                c_session.BeginTransaction();
                foreach (var i in listOfContainer)
                {
                    c_session.Delete(i);

                }
                c_session.Commit();



            }
            catch (Exception ex)
            {
                v_session.Rollback();
                c_session.Rollback();
                Log.Error(ex, "Vehicle Delete Error");
            }
            finally
            {
                v_session.CloseTransaction();
                c_session.CloseTransaction();

            }

            return Ok();
        }


    }
}
