using System.Collections.Generic;

namespace Timesheet.Domain
{
    public interface IAuthService
    {
        string Login(string lastName);
    }
}