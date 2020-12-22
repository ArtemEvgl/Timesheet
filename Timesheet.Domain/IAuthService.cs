using System.Collections.Generic;

namespace Timesheet.Domain
{
    public interface IAuthService
    {
        bool Login(string lastName);
    }
}