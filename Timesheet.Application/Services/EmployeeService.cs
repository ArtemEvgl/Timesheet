using Timesheet.Domain;
using Timesheet.Domain.Models;

namespace Timesheet.Application.Services
{
  
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public bool AddEmployee(Employee employee)
        {
            bool isValid = !string.IsNullOrEmpty(employee.LastName) && employee.Salary > 0;

            if (isValid)
            {
                _employeeRepository.AddEmployee(employee);
            }

            return isValid;
        }
    }
}
