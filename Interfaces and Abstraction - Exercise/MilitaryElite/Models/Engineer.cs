
namespace MilitaryElite.Models
{
    using Core.Models;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class Engineer : SpecialisedSoldier, IEngineer
    {
        private readonly ICollection<IRepairs> repairs;
        public Engineer(int id, string name, string lastName, decimal salary, Corps corps, ICollection<IRepairs> repairs) : base(id, name, lastName, salary, corps)
        {
            this.repairs = repairs;
        }

        public IReadOnlyCollection<IRepairs> Repairs => (IReadOnlyCollection<IRepairs>)this.repairs;

        public override string ToString()
        {


           

            StringBuilder sb = new StringBuilder();

            sb.AppendLine(base.ToString())
                .AppendLine("Repairs:");

            foreach (IRepairs item in repairs)
            {
                sb.AppendLine($"  {item.ToString()}");
            }

            return sb.ToString().TrimEnd();
        }
    }
}
