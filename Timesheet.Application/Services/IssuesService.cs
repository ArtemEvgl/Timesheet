using System;
using Timesheet.Domain;
using Timesheet.Domain.Models;

namespace Timesheet.BussinessLogic.Services
{
    public class IssuesService : IIssuesService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IIssuesClient _client;

        public IssuesService(IEmployeeRepository employeeRepository, 
            IIssuesClient client)
        {
            _employeeRepository = employeeRepository;
            _client = client;
        }

        public Issue[] Get()
        {
            var issues = _client.Get("user login").Result;
            return issues;
        }
    }
}
