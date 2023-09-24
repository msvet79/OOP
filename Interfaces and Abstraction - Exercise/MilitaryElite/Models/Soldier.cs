using MilitaryElite.Core.Models;

namespace MilitaryElite.Models
{
    public abstract class Soldier : ISoldier
    {
        protected Soldier(int id, string name, string lastName)
        {
            Id = id;
            Name = name;
            LastName = lastName;
        }
        public int Id { get; private set; }

        public string Name { get; private set; }

        public string LastName { get; private set; }

        public override string ToString()
        {
            return $"Name: {Name} {LastName} Id: {Id}";
        }

    }
}
