using System;
using System.Collections.Generic;
using System.Text;

namespace Vehicles.Exceptions
{
    public class InvalidVehicleException: Exception
    {
        private const string DefaultMessage = "Vehile type not supported";
        public InvalidVehicleException() :
            base(DefaultMessage)
        {

        }

        public InvalidVehicleException(string message)
            :base(message)
        {

        }
    }
}
