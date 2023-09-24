using System;
using System.Collections.Generic;
using System.Text;

namespace Telephony.Models.Interfaces
{
   public  interface ISmartphone : IStationaryphone
    {
        string BrowseUrl(string url);
    }
}
