using AccessControlSystem.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessControlSystem.Models.Contracts
{
    public class EmployeeRepository : IRepository<IEmployee>
    {
        private readonly List<IEmployee> _employeeList;
      

        public EmployeeRepository()
        {
            _employeeList = new List<IEmployee>();
        }

        public IReadOnlyCollection<IEmployee> Models
        {
            get
            {
                return _employeeList.AsReadOnly();
            }
        }


        public void AddNew(IEmployee model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model), "Employee model cannot be null.");
            }
            _employeeList.Add(model);
        }




        public IEmployee GetByName(string employeeName)
        {
            if (string.IsNullOrWhiteSpace(employeeName))
            {
                throw new ArgumentException("Model name cannot be null or empty.", nameof(employeeName));
            }
            var employee = _employeeList.FirstOrDefault(e => e.Name.Equals(employeeName, StringComparison.OrdinalIgnoreCase));
            return employee as Employee ?? (IEmployee?)null;
        }

  

    

        int IRepository<IEmployee>.SecurityCheck(string employeeName)
        {
            return GetByName(employeeName).Department.SecurityLevel;
        }

        
    }
}
