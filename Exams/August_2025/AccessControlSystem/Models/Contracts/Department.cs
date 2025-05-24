using AccessControlSystem.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessControlSystem.Models.Contracts
{
    public abstract class Department : IDepartment
    {
        private readonly List<string> employees;
        

        protected Department()
        {

            employees = new List<string>();
        }

        public int SecurityLevel { get; protected set; }
        public IReadOnlyCollection<string> Employees => employees.AsReadOnly();
        public int MaxEmployeesCount { get; protected set; }
        public void ContractEmployee(string employeeName)
        {
            if (employees.Count >= MaxEmployeesCount)
            {
                throw new ArgumentException(ExceptionMessages.InvalidDepartmentCapacity);
            }
            if (employees.Contains(employeeName))
            {
                throw new ArgumentException(ExceptionMessages.EmployeeAlreadyAdded);
            }
            employees.Add(employeeName);
        }
    }
    
    
}
