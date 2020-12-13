using System.Collections.Generic;
using Timesheet.Api.Models;
using static Timesheet.Api.Services.AuthService;

namespace Timesheet.Api.Services
{
    public class TimesheetService
    {
        public bool TrackTime(TimeLog timeLog)
        {
            bool isValid = timeLog.WorkingHours > 0 && timeLog.WorkingHours <= 24 && !string.IsNullOrWhiteSpace(timeLog.LastName);

            isValid = isValid && UserSessions.Sessions.Contains(timeLog.LastName);

            if (!isValid)
            {
                return false;
            }

            TimeSheets.TimeLogs.Add(timeLog);

            return true;
        }
    }

    public static class TimeSheets
    {
        public static List<TimeLog> TimeLogs { get; set; } = new List<TimeLog>();
    }
}
