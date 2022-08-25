using NHibernate;
using PycApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PycApi.Context
{
    public class ContainerMapperSession : ContainerIMapperSession
    {
        private readonly ISession session;
        private ITransaction transaction;

        public ContainerMapperSession(ISession session)
        {
            this.session = session;
        }

        public IQueryable<Containers> Containers => session.Query<Containers>();


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
            if(transaction!= null)
            {
                transaction.Rollback();
            }
            
        }

        public void CloseTransaction()
        {
            if (transaction != null)
            {
                transaction.Dispose();
                transaction = null;
            }
        }

        public void Save(Containers entity)
        {
            session.Save(entity);
        }
        public void Update(Containers entity)
        {
            session.Update(entity);
        }
        public void Delete(Containers entity)
        {
            session.Delete(entity);
        }
    }
}
