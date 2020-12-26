using System.Linq;
using Timesheet.Domain;
using Timesheet.Domain.Models;

namespace Timesheet.Application.Services
{
    public class ReportService : IReportService
    {
        private const decimal MAX_WORKING_HOURS_PER_MONTH = 160;
        private const decimal MAX_WORKING_HOURS_PER_DAY = 8;

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

            if (timeLogs == null || timeLogs.Length == 0)
            {
                return new EmployeeReport
                {
                    LastName = employee.LastName
                };
            }

            var totalHours = timeLogs.Sum(x => x.WorkingHours);
            decimal bill = 0;

            switch (lastName)
            {
                // staff
                case "Петров":
                {
                    var workingHoursGroupsByDay = timeLogs
                                            .GroupBy(x => x.Date.ToShortDateString());

                    foreach (var workingLogsPerDay in workingHoursGroupsByDay)
                    {
                        int dayHours = workingLogsPerDay.Sum(x => x.WorkingHours);

                        if (dayHours > MAX_WORKING_HOURS_PER_DAY)
                        {
                            var overtime = dayHours - MAX_WORKING_HOURS_PER_DAY;

                            bill += MAX_WORKING_HOURS_PER_DAY / MAX_WORKING_HOURS_PER_MONTH * employee.Salary;
                            bill += overtime / MAX_WORKING_HOURS_PER_MONTH * employee.Salary * 2;
                        }
                        else
                        {
                            bill += dayHours / MAX_WORKING_HOURS_PER_MONTH * employee.Salary;
                        }
                    }

                    break;
                }

                // manager
                case "Иванов":
                {
                    var workingHoursGroupsByDay = timeLogs
                                            .GroupBy(x => x.Date.ToShortDateString());

                    foreach (var workingLogsPerDay in workingHoursGroupsByDay)
                    {
                        int dayHours = workingLogsPerDay.Sum(x => x.WorkingHours);

                        if (dayHours > MAX_WORKING_HOURS_PER_DAY)
                        {
                            var overtime = dayHours - MAX_WORKING_HOURS_PER_DAY;

                            bill += MAX_WORKING_HOURS_PER_DAY / MAX_WORKING_HOURS_PER_MONTH * employee.Salary;
                            bill += overtime / MAX_WORKING_HOURS_PER_MONTH * employee.Salary * 2;
                        }
                        else
                        {
                            bill += dayHours / MAX_WORKING_HOURS_PER_MONTH * employee.Salary;
                        }
                    }

                    break;
                }

                // freelancer
                case "Сидоров":
                {
                    var workingHoursGroupsByDay = timeLogs
                                            .GroupBy(x => x.Date.ToShortDateString());

                    foreach (var workingLogsPerDay in workingHoursGroupsByDay)
                    {
                        int dayHours = workingLogsPerDay.Sum(x => x.WorkingHours);

                        if (dayHours > MAX_WORKING_HOURS_PER_DAY)
                        {
                            var overtime = dayHours - MAX_WORKING_HOURS_PER_DAY;

                            bill += MAX_WORKING_HOURS_PER_DAY / MAX_WORKING_HOURS_PER_MONTH * employee.Salary;
                            bill += overtime / MAX_WORKING_HOURS_PER_MONTH * employee.Salary * 2;
                        }
                        else
                        {
                            bill += dayHours / MAX_WORKING_HOURS_PER_MONTH * employee.Salary;
                        }
                    }

                    break;
                }

                default:
                    break;
            }

            return new EmployeeReport
            {
                LastName = employee.LastName,
                TimeLogs = timeLogs.ToList(),
                Bill = bill,
                TotalHours = totalHours,
                StartDate = timeLogs.Select(t => t.Date).Min(),
                EndDate = timeLogs.Select(t => t.Date).Max()
            };
        }

        private void ChangeBill(EmployeeReport report)
        {
            report.Bill = 10;

        }
    }
}
