using System;
using System.Collections.Generic;
using System.Text;
using Vehicles;


namespace Vehicles.Factories
{
    using Exceptions;
    using Models;
    using Models.Interfaces;
    public class VehicleFactory : IVehicleFactory
    {
        public VehicleFactory()
        {

        }
        public IVehicle CreateVehicle(string type, double fuelQty, double fuelConsuption)
        {
            IVehicle vehicle;

            if (type=="Car")
            {
                vehicle = new Car(fuelQty, fuelConsuption);
            }
            else if (type == "Truck")
            {
                vehicle = new Truck(fuelQty, fuelConsuption);
            }
            else
            {
                throw new InvalidVehicleException();
            }
            return vehicle;
        }
    }
}
