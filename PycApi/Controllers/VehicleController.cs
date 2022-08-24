using Microsoft.AspNetCore.Mvc;
using PycApi.Context;
using PycApi.Model;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PycApi.Controllers
{
    [ApiController]
    [Route("api/nhb/[controller]")]
    public class VehicleContoller : ControllerBase
    {
        private readonly IMapperSession session;
        public VehicleContoller(IMapperSession session)
        {
            this.session = session;
        }

        [HttpGet]
        public List<Vehicle> Get()
        {
            List<Vehicle> result = session.Vehicles.ToList();
            return result;
        }


        [HttpGet("{id}")]
        public Vehicle Get(int id)
        {
            Vehicle result = session.Vehicles.Where(x => x.Id == id).FirstOrDefault();
            return result;
        }

        [HttpPost]
        public void Post([FromBody] Vehicle vehicle)
        {
            try
            {
                session.BeginTransaction();
                session.Save(vehicle);
                session.Commit();
            }
            catch (Exception ex)
            {
                session.Rollback();
                Log.Error(ex, "Vehicle Insert Error");
            }
            finally
            {
                session.CloseTransaction();
            }
        }

        [HttpPut]
        public ActionResult<Vehicle> Put([FromBody] Vehicle request)
        {
            Vehicle vehicle = session.Vehicles.Where(x => x.Id == request.Id).FirstOrDefault();
            if (vehicle == null)
            {
                return NotFound();
            }

            try
            {
                session.BeginTransaction();

                vehicle.name = request.name;
                vehicle.plate = request.plate;


                session.Update(vehicle);

                session.Commit();
            }
            catch (Exception ex)
            {
                session.Rollback();
                Log.Error(ex, "Vehicle Insert Error");
            }
            finally
            {
                session.CloseTransaction();
            }


            return Ok();
        }


        [HttpDelete("{id}")]
        public ActionResult<Vehicle> Delete(int id)
        {
            Vehicle book = session.Vehicles.Where(x => x.Id == id).FirstOrDefault();
            if (book == null)
            {
                return NotFound();
            }

            try
            {
                session.BeginTransaction();
                session.Delete(book);
                session.Commit();
            }
            catch (Exception ex)
            {
                session.Rollback();
                Log.Error(ex, "Book Insert Error");
            }
            finally
            {
                session.CloseTransaction();
            }

            return Ok();
        }


    }
}
