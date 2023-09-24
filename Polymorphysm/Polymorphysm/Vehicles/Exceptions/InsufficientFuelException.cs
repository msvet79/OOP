using System;
using System.Collections.Generic;
using System.Text;

namespace Vehicles.Models.Exceptions
{
   public class InsufficientFuelException :Exception
    {
        public InsufficientFuelException()
        {

        }

        public InsufficientFuelException(string message)
            : base(message)
        {

        }
    }
}
