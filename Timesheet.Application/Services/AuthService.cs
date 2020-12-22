using System.Collections.Generic;
using Timesheet.Domain;
using Timesheet.Domain.Models;

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

            StaffEmployee staffEmployee = _employeeRepository.GetEmployee(lastName);
            var isEmployeeExist = staffEmployee != null ? true : false;

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
