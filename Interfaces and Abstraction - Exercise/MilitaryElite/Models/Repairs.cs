
using System;
using System.Collections.Generic;
using System.Text;

namespace MilitaryElite.Models
{
    using Core.Models;
    public class Repairs : IRepairs
    {
        private readonly string name;
        private readonly int hoursWorked;

        public Repairs(string name, int hoursWorked)
        {
            Name = name;
            HoursWorked = hoursWorked;
        }

        public string Name { get; private set; }

        public int HoursWorked { get; private set; }

        public override string ToString()
        {
            return $"Part Name: {Name} Hours Worked: {HoursWorked}";
        }
    }
}
