using Timesheet.Domain.Models;

namespace Timesheet.Domain
{
    public interface IEmployeeRepository
    {
        Employee GetEmployee(string lastName);
        void AddEmployee(Employee staffEmployee);
    }
}
