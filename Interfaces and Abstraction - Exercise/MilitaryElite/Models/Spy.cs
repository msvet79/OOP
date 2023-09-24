

namespace MilitaryElite.Models
{
    using Core.Models;
    using System;

    public class Spy : Soldier, ISpy
    {
        public Spy(int id, string name, string lastName, int codeNumber) : base(id, name, lastName)
        {
            CodeNumber = codeNumber;
        }

        public int CodeNumber { get; private set; }

        public override string ToString()
        {
            return base.ToString() + Environment.NewLine + $"Code Number: {CodeNumber}";
        }
    }
}
