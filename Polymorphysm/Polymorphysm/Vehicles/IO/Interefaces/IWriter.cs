using System;
using System.Collections.Generic;
using System.Text;

namespace Vehicles.IO.Interefaces
{
    public interface IWriter
    {
        public void Write(string message);

        public void WriteLine(string message);
    }
}
