using System.Collections.Generic;

namespace Timesheet.Domain
{
    public interface IAuthService
    {
        List<string> Employees { get; }

        bool Login(string lastName);
    }
}