using System;
using System.Collections.Generic;
using System.Text;
using Vehicles.IO.Interefaces;

namespace Vehicles.IO
{
    public class ConsoleWriter : IWriter
    {
        public void Write(string message) => Console.Write(message);


        public void WriteLine(string message) => Console.WriteLine(message);
       
    }
}
