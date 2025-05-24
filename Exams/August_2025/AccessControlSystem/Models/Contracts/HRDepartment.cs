using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessControlSystem.Models.Contracts
{
    public class HRDepartment : Department
    {
        public HRDepartment()
        {
            MaxEmployeesCount = 3; // Example capacity, adjust as needed
            SecurityLevel = 5; // Example security level, adjust as needed
        }
        public override string ToString()
        {
            return "HR Department";
        }
    }
}
