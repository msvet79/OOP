using System;
using System.Collections.Generic;
using System.Text;

namespace Vehicles.Models
{
    public class Truck : Vehicle
    {
        private const double FuelConsuptionIncrement = 1.6;
        public Truck(double fuelqunatity, double fuelComsuption) : base(fuelqunatity, fuelComsuption, FuelConsuptionIncrement)
        {
            
        }

        public override void Refuel(double liters)
        {
            base.Refuel(liters * 0.95);
        }
    }
}
