using Timesheet.Domain.Models;

namespace Timesheet.Domain
{
    public interface IIssuesService
    {
        Issue[] Get(string expectedLogin, string expectedProject);
    }
}
