using System.Linq;
using Timesheet.Domain;
using Timesheet.Domain.Models;

namespace Timesheet.Application.Services
{
    public class ReportService
    {
        private const int MAX_WORKING_HOURS_PER_MONTH = 160;

        private readonly ITimesheetRepository _timesheetRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public ReportService(ITimesheetRepository timesheetRepository, IEmployeeRepository employeeRepository)
        {
            _timesheetRepository = timesheetRepository;
            _employeeRepository = employeeRepository;
        }

        public EmployeeReport GetEmployeeReport(string lastName)
        {
            var employee = _employeeRepository.GetEmployee(lastName);
            var timeLogs = _timesheetRepository.GetTimeLogs(employee.LastName);

            //var hours = timeLogs.Sum(x => x.WorkingHours);
            //var bill = (hours / MAX_WORKING_HOURS_PER_MONTH) * employee.Salary;

            int monthHours = timeLogs[0].WorkingHours;
            decimal billPerHour = employee.Salary / 160;
            decimal bill = monthHours * billPerHour;                       
            for (int i = 1; i < timeLogs.Length; i++)
            {
                int dayHours = timeLogs[i].WorkingHours;
                if (timeLogs[i].Date.Month != timeLogs[i - 1].Date.Month)
                {
                    monthHours = 0;
                }
                monthHours += dayHours;
                if (monthHours <= 160)
                {
                    bill += timeLogs[i].WorkingHours * billPerHour;
                } else if (monthHours < 168)
                {
                    int overWorkHours = monthHours - MAX_WORKING_HOURS_PER_MONTH;
                    int simpleWorkHours = dayHours - overWorkHours;
                    bill += simpleWorkHours * billPerHour;
                    bill += overWorkHours * billPerHour * 2;
                } else
                {
                    bill += timeLogs[i].WorkingHours * billPerHour * 2;
                }
            }

            return new EmployeeReport
            {
                LastName = employee.LastName,
                TimeLogs = timeLogs.ToList(),
                Bill = bill
            };
        }
    }
}
