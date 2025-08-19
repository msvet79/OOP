using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessControlSystem.Models.Contracts
{
    internal class FinanceDepartment : Department

    {
        public FinanceDepartment()
        {
            MaxEmployeesCount = 3; 
            SecurityLevel = 4; 
        }
        public override string ToString()
        {
            return "FinanceDepartment";
        }
    }
}
