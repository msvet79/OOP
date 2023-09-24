namespace Telephony
{
    using Telephony.Core;
    using Telephony.Core.Interfaces;
    using Telephony.IO;
    using Telephony.IO.Interfaces;

    public class Program
    {
        
        static void Main(string[] args)
        {
            IReader reader = new ConsoleReader();
            IWriter writer = new ConsoleWriter();
            IEngine engine = new Engine(reader, writer);

            engine.Run();
        }
    }
}
