using System.Collections.Generic;
using Timesheet.Domain;

namespace Timesheet.Application.Services
{
    public class AuthService : IAuthService
    {
        IEmployeeRepository _employeeRepository;
        public AuthService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        
        public bool Login(string lastName)
        {
            if (string.IsNullOrWhiteSpace(lastName))
            {
                return false;
            }

            var Employee = _employeeRepository.GetEmployee(lastName);
            var isEmployeeExist = Employee != null;

            if (isEmployeeExist)
            {
                UserSessions.Sessions.Add(lastName);
            }

            return isEmployeeExist;
        }

        public static class UserSessions
        {

            public static HashSet<string> Sessions { get; set; } = new HashSet<string>();
        }
    }
}
