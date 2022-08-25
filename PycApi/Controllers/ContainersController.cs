using Microsoft.AspNetCore.Mvc;
using PycApi.Context;
using PycApi.Model;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;


namespace PycApi.Controllers
{
    [ApiController]
    [Route("api/nhb/[controller]")]
    public class ContainersContoller : ControllerBase
    {
        private readonly ContainerIMapperSession session;
        public ContainersContoller(ContainerIMapperSession session)
        {
            this.session = session;
        }

        [HttpGet]
        public List<Containers> Get()
        {
            List<Containers> result = session.Containers.ToList();
            return result;
        }


        [HttpGet("{id}")]
        public Containers Get(int id)
        {
            Containers result = session.Containers.Where(x => x.Id == id).FirstOrDefault();
            return result;
        }

        [HttpPost]
        public void Post([FromBody] Containers container)
        {
            try
            {
                session.BeginTransaction();
                session.Save(container);
                session.Commit();
            }
            catch (Exception ex)
            {
                session.Rollback();
                Log.Error(ex, "Container Insert Error");
            }
            finally
            {
                session.CloseTransaction();
            }
        }

        [HttpPut]
        public ActionResult<Vehicle> Put([FromBody] Containers request)
        {
            Containers container = session.Containers.Where(x => x.Id == request.Id).FirstOrDefault();
            if (container == null)
            {
                return NotFound();
            }

            try
            {
                session.BeginTransaction();

                container.containerName = request.containerName;
                container.latitude = request.latitude;
                container.longitude = request.longitude;


                session.Update(container);

                session.Commit();
            }
            catch (Exception ex)
            {
                session.Rollback();
                Log.Error(ex, "Containers Insert Error");
            }
            finally
            {
                session.CloseTransaction();
            }


            return Ok();
        }


        [HttpDelete("{id}")]
        public ActionResult<Containers> Delete(int id)
        {
            Containers book = session.Containers.Where(x => x.Id == id).FirstOrDefault();
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
