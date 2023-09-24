using System;


namespace Vehicles
{
    using Core;
    using Core.Interfaces;
    using IO.Interefaces;
    using Vehicles.Factories;
    using Vehicles.IO;

    public class StartUP
    {
        static void Main(string[] args)
        {
            IWriter writer = new ConsoleWriter();
            IReader reader = new ConsoleReader();
            IVehicleFactory vehiclefactory = new VehicleFactory();

            IEngine engine = new Engine(writer, reader, vehiclefactory);
            engine.Run();
                
        }
    }
}
