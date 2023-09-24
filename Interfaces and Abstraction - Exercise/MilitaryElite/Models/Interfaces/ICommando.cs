using System;
using System.Collections.Generic;
using System.Text;

namespace MilitaryElite.Core.Models
{
    public interface ICommando : ISpecialisedSoldier
    {
       
        IReadOnlyCollection<IMission> Missions { get; }
       
        
    }
}
