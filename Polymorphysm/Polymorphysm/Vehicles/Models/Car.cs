using System;
using System.Collections.Generic;
using System.Text;

namespace Vehicles.Models
{
    public class Car : Vehicle
    {
        private const double FuelConsuptionIncrement = 0.9;
        public Car(double fuelqunatity, double fuelComsuption) : base(fuelqunatity, fuelComsuption, FuelConsuptionIncrement)
        {
        }
    }
}
