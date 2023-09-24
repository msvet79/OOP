using System;
using System.Collections.Generic;
using System.Text;

namespace Telephony.Exceptions
{
   public  class InvalidUrlException : Exception
    {
        private const string DEFAULTURLEXEPTION = "Invalid URL!";

        public InvalidUrlException()
           : base(DEFAULTURLEXEPTION)
        {

        }

        public InvalidUrlException(string message)
        : base(message)
        {

        }
    }
}
