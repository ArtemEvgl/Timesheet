using System.Collections.Generic;

namespace Timesheet.Api.Services
{
    public interface IAuthService
    {
        List<string> Employees { get; }

        bool Login(string lastName);
    }
}