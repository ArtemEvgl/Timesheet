using System;
using System.Linq;

namespace Timesheet.Domain.Models
{
    public class FreelancerEmployee : Employee
    {
        public FreelancerEmployee(string lastName, decimal salary) : base(lastName, salary)
        {
        }
        public override decimal CalculateBill(TimeLog[] timeLogs)
        {
            var totalHours = timeLogs.Sum(x => x.WorkingHours);
            decimal bill = totalHours * Salary;
            return bill;
        }

        public override string GetPersonalData(char delimeter)
        {
            return $"{LastName}{delimeter}{Salary}{delimeter}Фрилансер{delimeter}\n";
        }

        public override bool CheckInputLog(TimeLog timeLog)
        {
            bool isValid = base.CheckInputLog(timeLog);
            isValid = timeLog.LastName == this.LastName && timeLog.Date > DateTime.Now.AddDays(-2) && isValid;
            return isValid;
        }
    }
}
