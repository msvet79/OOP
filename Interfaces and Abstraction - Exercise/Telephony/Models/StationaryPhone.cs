using System;
using System.Collections.Generic;
using System.Text;

namespace Telephony.Models
{
    using Interfaces;
    using System.Linq;
    using Telephony.Exceptions;

    public class Stationaryphone : IStationaryphone
    {
        public Stationaryphone()
        {

        }
        public string Call(string phoneNumber)
        {
            if (!isValidPhoneNumber(phoneNumber))
            {
                throw new InvalidPhoneNumberException();
            }

            return $"Dialing... {phoneNumber}";
        }


        private bool isValidPhoneNumber(string phoneNumber) => phoneNumber.All(p => char.IsDigit(p));
        
       
    }
}
