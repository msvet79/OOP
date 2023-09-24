
using System;
using System.Collections.Generic;
using System.Text;

namespace MilitaryElite.Core.Models
{
  
    public interface IEngineer : ISpecialisedSoldier
    {
       IReadOnlyCollection<IRepairs> Repairs { get; }
    }
}
