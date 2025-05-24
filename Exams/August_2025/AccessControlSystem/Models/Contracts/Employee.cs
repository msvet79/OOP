using AccessControlSystem.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessControlSystem.Models.Contracts
{
    public abstract class Employee : IEmployee
    {
        private string name = string.Empty; // Initialize to avoid CS8618
        private int securityId;
        private IDepartment? department; // Add backing field for Department

        protected Employee(string name, int securityId)
        {
            Name = name;
            SecurityId = securityId;
        }

        public string Name
        {
            get => name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.InvalidEmployeeName);
                }
                name = value;
            }
        }

        public IDepartment Department
        {
            get
            {
                if (department == null)
                {
                    throw new InvalidOperationException(ExceptionMessages.InvalidDepartmentName);
                }
                return department;
            }
            private set
            {
                department = value;
            }
        }

        public int SecurityId
        {
            get
            {
                return securityId;
            }
            private set
            {
                if (value is <= 100 or >= 999)
                {
                    throw new ArgumentException(ExceptionMessages.InvalidSecurityId);
                }
                securityId = value;
            }
        }

        public void AssignToDepartment(IDepartment department)
        {
            // Assign the employee to the specified department
            this.Department = department;
        }

        public override string ToString()
        {
            // Return a string representation of the employee
            return $"{Name} with security ID {SecurityId} is assigned to {Department.GetType().Name} with security level {Department.SecurityLevel}.";
        }
    }
}
