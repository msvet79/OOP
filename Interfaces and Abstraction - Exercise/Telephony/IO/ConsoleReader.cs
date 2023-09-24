using System;
using System.Collections.Generic;
using System.Text;


namespace Telephony.IO
{
    using Interfaces;
    public class ConsoleReader : IReader
    {
        public string Readline() => Console.ReadLine();
        
    }
}
