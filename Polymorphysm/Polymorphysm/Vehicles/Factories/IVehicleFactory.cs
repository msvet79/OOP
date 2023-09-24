using System;
using System.Collections.Generic;
using System.Text;
using Vehicles.Models.Interfaces;

namespace Vehicles.Factories
{
   public interface IVehicleFactory
    {
        IVehicle CreateVehicle(string type, double fuelQty, double fuelConsuption);
        
    }
}
