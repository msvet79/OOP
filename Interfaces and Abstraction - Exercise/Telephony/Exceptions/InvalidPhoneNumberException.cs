using System;
using System.Collections.Generic;
using System.Text;

namespace Telephony.Exceptions
{
    public class InvalidPhoneNumberException : Exception
    {
        private const string DEFAULTEXEPTIONMESSAGE = "Invalid number!";
        public InvalidPhoneNumberException()
            : base(DEFAULTEXEPTIONMESSAGE)
        {

        }

        public InvalidPhoneNumberException(string message)
            : base(message)
        {

        }
    }
}
