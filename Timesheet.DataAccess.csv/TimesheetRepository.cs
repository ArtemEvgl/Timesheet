using System;
using System.Collections.Generic;
using System.IO;
using Timesheet.Domain;
using Timesheet.Domain.Models;

namespace Timesheet.DataAccess.csv
{
    public class TimesheetRepository : ITimesheetRepository
    {
        private const char DELIMETER = ';';
        private const string PATH = "..\\Timesheet.DataAccess.csv\\Data\\timesheet.csv";

        public void Add(TimeLog timeLog)
        {
            var dataRow = $"{timeLog.Comment}{DELIMETER}" +
                $"{timeLog.Date}{DELIMETER}" +
                $"{timeLog.LastName}{DELIMETER}" +
                $"{timeLog.WorkingHours}\n";

            File.AppendAllText(PATH, dataRow);
        }

        public TimeLog[] GetTimeLogs(string lastName)
        {
            var data = File.ReadAllText(PATH);

            var timeLogs = new List<TimeLog>();

            foreach (var dataRow in data.Split('\n'))
            {
                var timeLog = new TimeLog();

                var dataMembers = dataRow.Split(DELIMETER);

                timeLog.Comment = dataMembers[0];
                timeLog.Date = DateTime.TryParse(dataMembers[1], out var date) ? date : new DateTime();
                timeLog.LastName = dataMembers[2];
                timeLog.WorkingHours = int.TryParse(dataMembers[3], out var workingHours) ? workingHours : 0;
            }

            return timeLogs.ToArray();
        }
    }
}
