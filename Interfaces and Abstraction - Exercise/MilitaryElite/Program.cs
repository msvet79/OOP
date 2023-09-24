
using System;

namespace MilitaryElite
{
    using IO;
    using MilitaryElite.Core;

    class Program
    {
        static void Main(string[] args)
        {
            IReader reader = new ConsoleReader();
            IWriter writer = new ConsoleWriter();

            IEngine engine = new Engine(writer,reader);
            engine.Run();
        }
    }
}
