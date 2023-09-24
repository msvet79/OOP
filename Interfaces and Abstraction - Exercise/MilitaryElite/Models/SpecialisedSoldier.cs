
using System;
using System.Collections.Generic;
using System.Text;

namespace MilitaryElite.Models
{
    using Core.Models;
    public abstract class SpecialisedSoldier : Private, ISpecialisedSoldier
    {
        protected SpecialisedSoldier(int id, string name, string lastName, decimal salary, Corps corps) : base(id, name, lastName, salary)
        {
            Corps = corps;
        }

        public Corps Corps {get; private set;}
        public override string ToString()
        {
            return base.ToString() + Environment.NewLine + $"Corps: {Corps}";

        }
    }
}
