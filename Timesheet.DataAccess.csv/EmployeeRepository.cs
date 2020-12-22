

using System.IO;
using Timesheet.Domain;
using Timesheet.Domain.Models;

namespace Timesheet.DataAccess.csv
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private const char DELIMETER = ';';
        private const string PATH = "..\\Timesheet.DataAccess.csv\\Data\\employees.csv";

        public void AddEmployee(StaffEmployee staffEmployee)
        {
            var dataRow = $"{staffEmployee.LastName}{DELIMETER}{staffEmployee.Salary}{DELIMETER}\n";
            File.AppendAllText(PATH, dataRow);
        }

        public StaffEmployee GetEmployee(string lastName)
        {
            var data = File.ReadAllText(PATH);
            var dataMembers = data.Split('\n');
            StaffEmployee staffEmployee = null;
            foreach (var dataMember in dataMembers)
            {
                if (dataMember.Contains(lastName))
                {
                    var fields = dataMember.Split(DELIMETER);
                    staffEmployee = new StaffEmployee()
                    {
                        LastName = fields[0],
                        Salary = decimal.TryParse(fields[1], out decimal salary) ? salary : 0
                    };
                }
            }
            return staffEmployee;
        }
    }
}
