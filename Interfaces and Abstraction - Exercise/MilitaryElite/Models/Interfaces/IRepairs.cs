using System;
using System.Collections.Generic;
using System.Text;

namespace MilitaryElite.Core.Models
{
    public class IRepairs
    {
        private readonly string name;
        private readonly int hoursWorked;

        //part name and hours worked(int).

        string Name => name;
        int HoursWorked => hoursWorked;
    }
}
