using Timesheet.Domain.Models;

namespace Timesheet.Domain
{
    public interface IReportService
    {
        EmployeeReport GetEmployeeReport(string lastName);
    }
}
