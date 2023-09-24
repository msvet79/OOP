using System;
using System.Collections.Generic;
using System.Text;

namespace MilitaryElite.Core.Models
{
    public interface ISoldier
    {
        //id, first name, and last name

        int Id { get; }

        string Name { get; }

        string LastName { get; }
    }
}
