using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
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

            var Employee = _employeeRepository.GetEmployee(lastName);
            var isEmployeeExist = Employee != null;

            if (isEmployeeExist)
            {
                UserSessions.Sessions.Add(lastName);
            }

            return isEmployeeExist;
        }

        //"eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJVc2VyTmFtZSI6IlRlc3RMYXN0TmFtZSIsIm5iZiI6MTYxMTI0ODY3OSwiZXhwIjoxNjExMjUyMjc5LCJpYXQiOjE2MTEyNDg2NzksImF1ZCI6IkNoaWVmIn0.rESb7qe1OK94WEuDLTXO0P-opCE91b8WJwV49w_4hvc"
        public string GenerateJWT(string secret, Employee employee)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = System.Text.Encoding.UTF8.GetBytes(secret);

            var descriptor = new SecurityTokenDescriptor
            {
                Audience = employee.Position,
                Claims = new Dictionary<string, object> { 
                    { "UserName", employee.LastName} 
                },
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };
            
            SecurityToken token = tokenHandler.CreateToken(descriptor);

            return tokenHandler.WriteToken(token);
        }

        public static class UserSessions
        {

            public static HashSet<string> Sessions { get; set; } = new HashSet<string>();
        }
    }
}
