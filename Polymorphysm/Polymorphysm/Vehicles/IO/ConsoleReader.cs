using System;
using System.Collections.Generic;
using System.Text;

namespace Vehicles.IO.Interefaces
{
    public class ConsoleReader : IReader
    {
        public string Readline() => Console.ReadLine();
        
    }
}
