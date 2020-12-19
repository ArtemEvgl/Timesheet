using Timesheet.Domain.Models;

namespace Timesheet.Domain
{
    public interface IEmployeeRepository
    {
        StaffEmployee GetEmployee(string lastName);
    }
}
