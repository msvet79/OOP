using AccessControlSystem.Models.Contracts;
using AccessControlSystem.Utilities.Messages;
using System.Collections.Generic;


namespace AccessControlSystem.Core.Contracts
{
    internal class Controller : IController
    {
        private readonly EmployeeRepository _employees;
        private IEmployee? employee;


        private readonly Dictionary<string,IDepartment> _departments;
        public Controller()

        {
            _employees = new();
            _departments = new Dictionary<string,IDepartment>();
            employee = null;
        }
        string IController.AddDepartment(string departmentTypeName)
        {

            if (!(departmentTypeName == nameof(ITDepartment) || departmentTypeName == nameof(HRDepartment) || departmentTypeName == nameof(FinanceDepartment)))
            {
                return string.Format(System.Globalization.CultureInfo.InvariantCulture, OutputMessages.InvalidDepartmentType, departmentTypeName);
            }
            if (_departments.ContainsKey(departmentTypeName))
            {
                return string.Format(System.Globalization.CultureInfo.InvariantCulture, OutputMessages.DepartmentExists, departmentTypeName);
            }
            else
            {

                if (departmentTypeName == nameof(ITDepartment))
                {
                    _departments.Add(departmentTypeName, new ITDepartment());
                }
                else if (departmentTypeName == nameof(HRDepartment))
                {
                    _departments.Add(departmentTypeName, new HRDepartment());
                }
                else if (departmentTypeName == nameof(FinanceDepartment))
                {
                    _departments.Add(departmentTypeName, new FinanceDepartment());
                }
                
                return string.Format(System.Globalization.CultureInfo.InvariantCulture, OutputMessages.DepartmentAdded, departmentTypeName);
            }

        }


        public string AddEmployeeToApplication(string employeeName, string employeeTypeName, int securityId)
        {
            if (employeeTypeName is not ((nameof(GeneralEmployee)) or (nameof(ITSpecialist))))
            {
                return string.Format(System.Globalization.CultureInfo.InvariantCulture, OutputMessages.InvalidEmployeeType, employeeTypeName);
            }

            if (_employees.GetByName(employeeName) != null)
            {
                return string.Format(System.Globalization.CultureInfo.InvariantCulture, OutputMessages.EmployeeExistsInApplication, employeeName);
            }

            if (_employees.Models.Any(e => e.SecurityId == securityId))
            {
                return string.Format(System.Globalization.CultureInfo.InvariantCulture, OutputMessages.SecurityIdExists, securityId);
            }

            if (employeeTypeName == nameof(GeneralEmployee))
            {
                employee = new GeneralEmployee(employeeName, securityId);
            }
            else
            {
                employee = new ITSpecialist(employeeName, securityId);
            }

            _employees.AddNew(employee);

            return string.Format(System.Globalization.CultureInfo.InvariantCulture, OutputMessages.EmployeeAddedToApplication, employeeName);
        }




        string IController.AddEmployeeToDepartment(string employeeName, string departmentTypeName)
        {
            // Assuming _employees is a collection that holds employee data
            var employee = _employees.GetByName(employeeName);
            if (employee == null)
            {
                return string.Format(OutputMessages.EmployeeNotInApplication, employeeName);
            }

            if (!(departmentTypeName == nameof(ITDepartment) || departmentTypeName == nameof(HRDepartment) || departmentTypeName == nameof(FinanceDepartment)))
            {
                return string.Format(System.Globalization.CultureInfo.InvariantCulture, OutputMessages.InvalidDepartmentType, departmentTypeName);
            }

            

            string? employeeTypeName = employee.GetType().Name;
           
            if (!(employeeTypeName == nameof(ITSpecialist) && departmentTypeName== nameof(ITDepartment)))
            {
                return string.Format(System.Globalization.CultureInfo.InvariantCulture, OutputMessages.ContractNotAllowed, employeeName, departmentTypeName);
            }

            if (!(employeeTypeName == nameof(GeneralEmployee) && (departmentTypeName == nameof(HRDepartment)|| departmentTypeName == nameof(FinanceDepartment))))
            {
               
                
                return string.Format(System.Globalization.CultureInfo.InvariantCulture, OutputMessages.ContractNotAllowed, employeeName, departmentTypeName);
                
            }
           
            IDepartment? department = _departments.TryGetValue(departmentTypeName, out var dept) ? dept : null;

            if (department == null)
            {
                return string.Format(System.Globalization.CultureInfo.InvariantCulture, OutputMessages.DepartmentIsNotAvailable, departmentTypeName);
            }




            if (_departments.Any(e => e.Value.Employees.Contains(employeeName)))
                {
                    return string.Format(System.Globalization.CultureInfo.InvariantCulture, OutputMessages.EmployeeExistsInDepartment, employeeName);
                }
            
            if (department.MaxEmployeesCount <= department.Employees.Count)
            {
                return string.Format(System.Globalization.CultureInfo.InvariantCulture, OutputMessages.DepartmentFull, departmentTypeName);
            }

        
            employee.AssignToDepartment(departmentTypeName);
            return "Employee added to department successfully.";
        }

          
        


        string IController.AddSecurityZone(string securityZoneName, int accessLevelRequired)
        {
            throw new NotImplementedException();
        }

        string IController.AuthorizeAccess(string securityZoneName, string employeeName)
        {
            throw new NotImplementedException();
        }

        string IController.SecurityReport()
        {
            throw new NotImplementedException();
        }
    }
}
