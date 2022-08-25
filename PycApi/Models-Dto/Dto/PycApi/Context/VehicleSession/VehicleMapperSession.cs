using NHibernate;
using PycApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PycApi.Context.VehicleSession
{
    public class VehicleMapperSession : VehicleIMapperSession
    {
        private readonly ISession session;
        private ITransaction transaction;

        public VehicleMapperSession(ISession session)
        {
            this.session = session;
        }

        public IQueryable<Vehicle> Vehicles => session.Query<Vehicle>();


        public void BeginTransaction()
        {
            transaction = session.BeginTransaction();
        }

        public void Commit()
        {
            transaction.Commit();
        }

        public void Rollback()
        {
            transaction.Rollback();
        }

        public void CloseTransaction()
        {
            if (transaction != null)
            {
                transaction.Dispose();
                transaction = null;
            }
        }

        public void Save(Vehicle entity)
        {
            session.Save(entity);
        }
        public void Update(Vehicle entity)
        {
            session.Update(entity);
        }
        public void Delete(Vehicle entity)
        {
            session.Delete(entity);
        }
    }
}
