using PycApi.Model;
using System.Collections.Generic;


namespace PycApi.Context
{
    public interface IContainerRepository
    {
        
        Containers GetById(int id);
        IEnumerable<Containers> GetAll();
        void Add(Containers container);
        void Delete(Containers container);
        bool Update(Containers container);
        void DeleteAll(List<Containers> listOfContainer);
    }
}
