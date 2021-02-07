using System;
using System.Threading.Tasks;
using Timesheet.Domain.Models;

namespace Timesheet.Domain
{
    public interface IIssuesClient
    {
        Task<Issue[]> Get(string login, string expectedProject);
    }
}
