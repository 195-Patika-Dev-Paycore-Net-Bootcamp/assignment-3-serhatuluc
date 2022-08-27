using PycApi.Model;
using System.Collections.Generic;


namespace PycApi.Context
{
    public interface IVehicleRepository
    {
        
        Vehicle GetById(int id);
        IEnumerable<Vehicle> GetAll();
        void Add(Vehicle vehicle);
        void Delete(Vehicle vehicle);
        bool Update(Vehicle vehicle);
    }
}
