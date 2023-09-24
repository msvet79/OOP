using System;
using System.Collections.Generic;
using System.Text;
using Vehicles;

namespace Vehicles.Core
{
    using Interfaces;
    using System.Linq;
    using Factories;

    using IO.Interefaces;
    using Models.Interfaces;
    using Exceptions;
    using Models.Exceptions;

    public class Engine : IEngine
    {
        private readonly IWriter writer;
        private readonly IReader reader;
        private readonly IVehicleFactory vehicleFactory;

        private readonly ICollection<IVehicle> vehicles;

        public Engine(IWriter writer, IReader reader, IVehicleFactory vehicleFactory)
            :this()
        {
            this.writer = writer;
            this.reader = reader;
            this.vehicleFactory = vehicleFactory;
            this.vehicles = new HashSet<IVehicle>();
        }

        private Engine()
        {
            this.vehicles = new HashSet<IVehicle>();
        }

        public void Run()
        {


            vehicles.Add(BuildVehicleUsingFactory());
            vehicles.Add(BuildVehicleUsingFactory());

            int n = int.Parse(reader.Readline());

            for (int i = 0; i < n; i++)
            {
                try
                {
                    ProcessCommand();
                }
                catch (InsufficientFuelException ife)
                {

                    this.writer.WriteLine(ife.Message);
                }
                catch (InvalidVehicleException ive)
                {
                    this.writer.WriteLine(ive.Message);
                }
                catch (Exception)
                {
                    throw;
                }

            }

            PrintAll();
        }

        private void PrintAll()
        {
            foreach (IVehicle vehicle in this.vehicles)
            {
                writer.WriteLine(vehicle.ToString());
            }
        }

        private IVehicle BuildVehicleUsingFactory()
        {
            string[] vahicleArg = reader.Readline().Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            string vehilcetype = vahicleArg[0];
            double vehilceFuelQty = double.Parse(vahicleArg[1]);
            double vehilceFuelConsuption = double.Parse(vahicleArg[2]);
            IVehicle vehicle = this.vehicleFactory.CreateVehicle(vehilcetype, vehilceFuelQty, vehilceFuelConsuption);
            return vehicle;
        }

        private void ProcessCommand()
        {
            string[] cmdArg = reader.Readline()
                   .Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);

            string cmdType = cmdArg[0];
            string vehicleType = cmdArg[1];
            double arg = double.Parse(cmdArg[2]);

            IVehicle processVehile = vehicles.FirstOrDefault(p => p.GetType().Name == vehicleType);
            if (processVehile == null)
            {
                throw new InvalidVehicleException();
            }

            if (cmdType == "Drive")
            {
                writer.WriteLine(processVehile.Drive(arg));
            }
            else if (cmdType == "Refuel")
            {
                processVehile.Refuel(arg);
            }
        }
    }
}
