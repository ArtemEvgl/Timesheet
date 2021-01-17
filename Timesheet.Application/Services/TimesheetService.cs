using System;
using Timesheet.Domain;
using Timesheet.Domain.Models;
using static Timesheet.Application.Services.AuthService;

namespace Timesheet.Application.Services
{
    public class TimesheetService : ITimeSheetService
    {
        private readonly ITimesheetRepository _timesheetRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public TimesheetService(ITimesheetRepository timesheetRepository, IEmployeeRepository employeeRepository)
        {
            _timesheetRepository = timesheetRepository;
            _employeeRepository = employeeRepository;
        }

        public bool TrackTime(TimeLog timeLog, string lastName)
        {
            //bool isValid = timeLog.WorkingHours > 0
            //    && timeLog.WorkingHours <= 24
            //    && !string.IsNullOrWhiteSpace(timeLog.LastName);

            var employee = _employeeRepository.GetEmployee(lastName);
            
            bool isValid = employee != null ? employee.CheckInputLog(timeLog) : false;

            if (!isValid)
            {
                return false;
            }

            //if (employee is FreelancerEmployee)
            //{
            //    if (DateTime.Now.AddDays(-2) > timeLog.Date)
            //    {
            //        return false;
            //    }
            //}

            //if (employee is FreelancerEmployee || employee is StaffEmployee)
            //{
            //    if (lastName != timeLog.LastName)
            //    {
            //        return false;
            //    }
            //}
            
            _timesheetRepository.Add(timeLog);

            return true;
        }
    }
}
