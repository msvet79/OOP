using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessControlSystem.Models.Contracts
{
    public class GeneralEmployee : Employee, IEmployee
    {
 

        public GeneralEmployee(string name, int securityId) : base(name, securityId)
        {
        }
    }
}
