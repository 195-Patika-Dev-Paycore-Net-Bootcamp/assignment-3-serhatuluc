using PycApi.Model;
using System.Linq;


namespace PycApi.Context
{
    public interface ContainerIMapperSession
    {
        void BeginTransaction();
        void Commit();
        void Rollback();
        void CloseTransaction();
        void Save(Containers entity);
        void Update(Containers entity);
        void Delete(Containers entity);

        IQueryable<Containers> Containers { get; }
    }
}
