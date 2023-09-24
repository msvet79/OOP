using System;
using System.Collections.Generic;
using System.Text;


namespace Vehicles.Models
{
    using Interfaces;
    using Vehicles.Exceptions;
    using Vehicles.Models.Exceptions;

    public abstract class Vehicle : IVehicle
    {

        protected Vehicle(double fuelqunatity, double fuelComsuption, double fuelConsuptionIncrement)
        {
            this.FuelQuantity = fuelqunatity;
            this.FuelConsumption = fuelComsuption + fuelConsuptionIncrement;

        }
        public double FuelQuantity { get; private set; }

        public double FuelConsumption { get; private set; }

        public string Drive(double distance)
        {
            double neededFuel = distance * FuelConsumption;

            if (neededFuel > this.FuelQuantity)
            {
                throw new InsufficientFuelException(string.Format(ExceptionMessages.InsufficientFuelExceptionMessage, this.GetType().Name));
            }

            FuelQuantity -= neededFuel;

           // Console.WriteLine($"{this.GetType().Name} travelled {distance} km");
           return $"{this.GetType().Name} travelled {distance} km";
        }

        public virtual void Refuel(double liters)
        {
            this.FuelQuantity += liters;
        }

        public override string ToString()
        {
            return $"{this.GetType().Name}: {FuelQuantity:f2}";
        }
    }
}
