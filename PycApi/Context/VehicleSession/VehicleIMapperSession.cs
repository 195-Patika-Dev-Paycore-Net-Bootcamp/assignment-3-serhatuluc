﻿using PycApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PycApi.Context.VehicleSession
{
    public interface VehicleIMapperSession
    {
        void BeginTransaction();
        void Commit();
        void Rollback();
        void CloseTransaction();
        void Save(Vehicle entity);
        void Update(Vehicle entity);
        void Delete(Vehicle entity);

        IQueryable<Vehicle> Vehicles { get; }
    }
}