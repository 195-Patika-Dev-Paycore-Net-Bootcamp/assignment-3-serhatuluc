using NHibernate;
using PycApi.Model;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;


namespace PycApi.Context
{

    public class VehicleRepository : IVehicleRepository
    {
        private readonly ISession session;
        private ITransaction transaction;

        public VehicleRepository(ISession session)
        {
            this.session = session;
        }
        //Each method has been created by using transaction and session.
        //Having similar method as EF will make controllers more readable
        public void Add(Vehicle vehicle)  //This method is used to add container
        {
            try
            {
                transaction = session.BeginTransaction();
                session.Save(vehicle);
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                Log.Error(ex, "Vehicle Insert Error");
            }
            finally
            {
                session.Dispose();
            }
        }

        public void Delete(Vehicle vehicle) //Method is used to delete container
        {
            
            try
            {
                transaction = session.BeginTransaction();
                session.Delete(vehicle);
                transaction.Commit();
            }
            catch (Exception ex)
            {
               
                Log.Error(ex, "Vehicle Delete Error");
            }
            finally
            {
                session.Dispose();
            }
        }

        public IEnumerable<Vehicle> GetAll() //Listing all containers 
        {
            var listOfcontainers = session.Query<Vehicle>().ToList();
            return listOfcontainers;
        }

        public Vehicle GetById(int id) //Request container by id 
        {
            return session.Query<Vehicle>().Where(x => x.Id == id).FirstOrDefault();
        }

        public bool Update(Vehicle vehicle) //Updating the container
        {
            try
            {
                transaction = session.BeginTransaction();
                session.Update(vehicle);
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                Log.Error(ex, "Vehicle Update Error");
                return false;
            }
            finally
            {
                session.Dispose();
            }
            return true;
        }

    }
}

