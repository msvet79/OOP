

namespace MilitaryElite.Models
{
    using Core.Models;
    using System.Collections.Generic;
    using System.Text;

    public class LieutenantGeneral : Private, ILieutenantGeneral
    {
        private readonly ICollection<IPrivate> privates;
        public LieutenantGeneral(int id, string name, string lastName, decimal salary, ICollection<IPrivate> privates) : base(id, name, lastName, salary)
         
        {
            this.privates = privates;

        }


        public IReadOnlyCollection<IPrivate> Privates => (IReadOnlyCollection<IPrivate>)privates;

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine(base.ToString()).AppendLine("Privates:");

            foreach (var item in this.privates)
            {
                sb.AppendLine($"  {item.ToString()}");
            }

            return sb.ToString().TrimEnd();
        }


    }
}
