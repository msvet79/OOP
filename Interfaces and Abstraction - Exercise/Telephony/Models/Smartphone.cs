using System;
using System.Collections.Generic;
using System.Text;


namespace Telephony.Models
{
    using Interfaces;
    using System.Linq;
    using Telephony.Exceptions;

    public class Smartphone : ISmartphone
    {
        public string BrowseUrl(string url)
        {
            if (ValidateUrl(url))
            {
                throw new InvalidUrlException();
            }

            return $"Browsing: {url}!";
        }

        public string Call(string phoneNumber)
        {
            if (!isValidPhoneNumber(phoneNumber))
            {
                throw new InvalidPhoneNumberException();
            }

            return $"Calling... {phoneNumber}";
        }

        private bool isValidPhoneNumber(string phoneNumber) => phoneNumber.All(p => char.IsDigit(p));

        private bool ValidateUrl(string url) => url.Any(p => char.IsDigit(p));
    }
}
