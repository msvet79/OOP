using AccessControlSystem.Models.Contracts;
using AccessControlSystem.Utilities.Messages;
using System.Collections.Generic;
using System.Xml.Linq;


namespace AccessControlSystem.Core.Contracts
{
    internal class Controller : IController
    {
        private readonly EmployeeRepository _employees;
        private readonly SecurityZoneRepository _securityZones;
        private IEmployee? employee;
        private readonly Dictionary<string, IDepartment> _departments;

        public Controller()
        {
            _employees = new();
            _departments = new Dictionary<string, IDepartment>();
            employee = null;
            _securityZones = new SecurityZoneRepository();
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

            string? employeeTypeName = employee.GetType().Name;

            switch (departmentTypeName)
            {
                case nameof(ITDepartment):
                    if (!(employeeTypeName == nameof(ITSpecialist)))
                    {
                        return string.Format(System.Globalization.CultureInfo.InvariantCulture, OutputMessages.ContractNotAllowed, employeeTypeName, departmentTypeName);
                        
                    }
                    break;
                case nameof(HRDepartment):
                    if (!(employeeTypeName == nameof(GeneralEmployee)))
                    {
                        return string.Format(System.Globalization.CultureInfo.InvariantCulture, OutputMessages.ContractNotAllowed, employeeTypeName, departmentTypeName);
                    }
                    break;
                case nameof(FinanceDepartment):
                    if (!(employeeTypeName == nameof(GeneralEmployee)))
                    {
                        return string.Format(System.Globalization.CultureInfo.InvariantCulture, OutputMessages.ContractNotAllowed, employeeTypeName, departmentTypeName);
                    }
                    break;
                default:
                    return string.Format(System.Globalization.CultureInfo.InvariantCulture, OutputMessages.InvalidDepartmentType, departmentTypeName);
            }


            IDepartment? department = _departments.TryGetValue(departmentTypeName, out department) ? department : null;

            if (department is null)
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

            department.ContractEmployee(employeeName);
            employee.AssignToDepartment(department);
            return string.Format(OutputMessages.EmployeeAddedToDepartment, employeeTypeName, departmentTypeName);
        }





        string IController.AddSecurityZone(string securityZoneName, int accessLevelRequired)
        {



            if (_securityZones.GetByName(securityZoneName) != null)
            {
                return string.Format(System.Globalization.CultureInfo.InvariantCulture, OutputMessages.SecurityZoneExists, securityZoneName);
            }
            _securityZones.AddNew(new SecurityZone(securityZoneName, accessLevelRequired));

            // Return a success message
            return string.Format(System.Globalization.CultureInfo.InvariantCulture, OutputMessages.SecurityZoneAdded, securityZoneName);
        }

        string IController.AuthorizeAccess(string securityZoneName, string employeeName)
        {
            var securityZone = _securityZones.GetByName(securityZoneName);
            if (securityZone == null)
            {
                return string.Format(System.Globalization.CultureInfo.InvariantCulture, OutputMessages.SecurityZoneNotFound, securityZoneName);
            }

            var employee = _employees.GetByName(employeeName);

            if (employee == null)
            {
                return string.Format(System.Globalization.CultureInfo.InvariantCulture, OutputMessages.EmployeeNotInApplication, employeeName);
            }

            if (employee.Department is null || employee.Department.SecurityLevel < securityZone.AccessLevelRequired)
            {
                return string.Format(System.Globalization.CultureInfo.InvariantCulture, OutputMessages.AccessDenied, employeeName, securityZoneName);
            }

            if (securityZone.AccessLog.Contains(employee.SecurityId))
            {
                return string.Format(System.Globalization.CultureInfo.InvariantCulture, OutputMessages.EmployeeAlreadyAuthorized, employeeName, securityZoneName);
            }

            securityZone.LogAccessKey(employee.SecurityId);
            return string.Format(System.Globalization.CultureInfo.InvariantCulture, OutputMessages.EmployeeAuthorized, employeeName, securityZoneName);
        }


        string IController.SecurityReport()
        {
            System.Text.StringBuilder stringBuilderReport = new();
            stringBuilderReport.AppendLine("Security Report:");

            foreach (ISecurityZone securityZone in _securityZones.Models.OrderByDescending(sz => sz.AccessLevelRequired).ThenBy(sz => sz.Name))
            {
                _ = stringBuilderReport.AppendLine($"-{securityZone.Name} (Access level required: {securityZone.AccessLevelRequired})");

                foreach (int securityId in securityZone.AccessLog)
                {
                    IEmployee? employee = _employees.Models.FirstOrDefault(e => e.SecurityId == securityId);
                    if (employee != null)
                    {
                        _ = stringBuilderReport.AppendLine($"--" + employee.ToString());
                    }
                }
            }

            return stringBuilderReport.ToString();
        }
    }
}


