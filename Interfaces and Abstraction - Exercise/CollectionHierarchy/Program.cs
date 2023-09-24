using CollectionHierarchy.Core;
using CollectionHierarchy.Core.Interfaces;
using CollectionHierarchy.IO;
using System;

namespace CollectionHierarchy
{
    class Program
    {
        static void Main(string[] args)
        {
            IReader reader = new ConsoleReader();
            IWriter writer = new ConsoleWriter();

            IEngine engine = new Engine(writer, reader);
            engine.Run();
        }
    }
}
