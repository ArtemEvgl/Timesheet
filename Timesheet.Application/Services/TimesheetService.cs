using System.Collections.Generic;
using Timesheet.Domain;
using Timesheet.Domain.Models;
using static Timesheet.Application.Services.AuthService;

namespace Timesheet.Application.Services
{
    public class TimesheetService : ITimeSheetService
    {
        public bool TrackTime(TimeLog timeLog)
        {
            bool isValid = timeLog.WorkingHours > 0 && timeLog.WorkingHours <= 24 && !string.IsNullOrWhiteSpace(timeLog.LastName);

            isValid = UserSessions.Sessions.Contains(timeLog.LastName) && isValid;

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
