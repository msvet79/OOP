using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessControlSystem.Models.Contracts
{
    public class ITDepartment : Department
    {
        public ITDepartment()
        {
            MaxEmployeesCount = 8; 
            SecurityLevel = 5; 
        }
        public override string ToString()
        {
            return "ITDepartment";
        }
    }

}

