using System;
using System.Collections.Generic;
using System.Text;


namespace Telephony.Core
{
    using Interfaces;
    using Telephony.Exceptions;
    using Telephony.IO.Interfaces;
    using Telephony.Models;
    using Telephony.Models.Interfaces;

    public class Engine : IEngine
    {
        private readonly IReader reader;
        private readonly IWriter writer;

        private readonly ISmartphone smartPhone;
        private readonly IStationaryphone stationaryPhone;

        public Engine(IReader reader, IWriter writer) : this()
        {
            this.reader = reader;
            this.writer = writer;
        }

        private Engine()

        {
            this.smartPhone = new Smartphone();
            this.stationaryPhone = new Stationaryphone();
        }
        public void Run()
        {

            
                string[] phoneNumebrs = this.reader.Readline()
               .Split(' ');

                string[] urls = this.reader.Readline()
                    .Split(' ');


            
                foreach (string item in phoneNumebrs)
                {
                try
                {
                    if (item.Length == 10)
                    {
                        writer.Writeln(smartPhone.Call(item));
                    }
                    else if (item.Length == 7)
                    {
                        writer.Writeln(stationaryPhone.Call(item));
                    }
                    else
                    {
                        throw new InvalidPhoneNumberException();
                    }
                }
                catch (InvalidPhoneNumberException ex)
                {

                    this.writer.Writeln(ex.Message);
                }
                
               catch (Exception)
                {

                    throw;
                }


                }


                foreach (string item in urls)
                {
                try
                {
                    writer.Writeln(smartPhone.BrowseUrl(item));
                }
                catch (InvalidUrlException ex)
                {

                    writer.Writeln(ex.Message);
                }
                catch (Exception)
                {

                    throw;
                }

            }
            }
            
    }
}
