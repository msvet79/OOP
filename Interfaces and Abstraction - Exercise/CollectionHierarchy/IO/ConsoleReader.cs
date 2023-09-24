using System;
using System.Collections.Generic;
using System.Text;

namespace CollectionHierarchy.IO
{
    public class ConsoleReader : IReader
    {
        public string Readline() => Console.ReadLine();
        
    }
}
