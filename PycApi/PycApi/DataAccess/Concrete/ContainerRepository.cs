using NHibernate;
using PycApi.Model;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;


namespace PycApi.Context
{

    public class ContainerRepository : IContainerRepository
    {
        private readonly ISession session;
        private ITransaction transaction;

        public ContainerRepository(ISession session)
        {
            this.session = session;
        }
        //Each method has been created by using transaction and session.
        //Having similar method as EF will make controllers more readable
        public void Add(Containers container)   //This method is used to add container
        {
            
            try
            {
                transaction = session.BeginTransaction();
                session.Save(container);    //To save the container
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                Log.Error(ex, "Container Insert Error");
            }
            finally
            {
                session.Dispose();
            }
        }

        public void Delete(Containers container) //Method is used to delete container
        {
            if (container is null)
            {
                return;
            }
            try
            {
                transaction = session.BeginTransaction();
                session.Delete(container);  //Deleting it here
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                Log.Error(ex, "Container Delete Error");
            }
            finally
            {
                session.Dispose();
            }
        }

        public IEnumerable<Containers> GetAll() //Listing all containers 
        {
            var listOfcontainers = session.Query<Containers>().ToList();
            return listOfcontainers;
        }

        public Containers GetById(int id) //Request container by id 
        {
            return session.Query<Containers>().Where(x=>x.Id == id).FirstOrDefault();
        }

        public bool Update(Containers container) //Updating the container
        {
            try
            {
                transaction = session.BeginTransaction();
                session.Update(container);
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                Log.Error(ex, "Container Update Error");
                return false;
            }
            finally
            {
                session.Dispose();
            }
            return true;
        }
        //This method is used where it is asked to delete a vehicle.
        //Method deletes the list of containers which has id of vehicle that is asked to be deleted
        public void DeleteAll(List<Containers> listOfContainer) //Delete a list of containers
        {
            if (listOfContainer is null)
            {
                return;
            }
            try
            {
                transaction = session.BeginTransaction();

                //Looping through list of containers to delete
                foreach(var i in listOfContainer)
                {
                    session.Delete(i);
                }
               
                transaction.Commit();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Container Delete Error");
            }
           
          
        }

    }
}

