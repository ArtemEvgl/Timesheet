using Timesheet.Domain;
using Timesheet.Domain.Models;

namespace Timesheet.DataAccess.MSSQL.Repositories
{
    public class TimesheetRepository : ITimesheetRepository
    {
        public void Add(TimeLog timeLog)
        {
            throw new System.NotImplementedException();
        }

        public TimeLog[] GetTimeLogs(string lastName)
        {
            throw new System.NotImplementedException();
        }
    }
}
