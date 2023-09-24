using System;
using System.Collections.Generic;
using System.Text;

namespace Telephony.IO
{
    using Interfaces;
    public class ConsoleWriter : IWriter
    {
        public void Write(string text) => Console.Write(text);


        public void Writeln(string text) => Console.WriteLine(text);
        
    }
}
