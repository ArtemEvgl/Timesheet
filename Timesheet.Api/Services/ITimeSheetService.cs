using Timesheet.Api.Models;

namespace Timesheet.Api.Services
{
    public interface ITimeSheetService
    {
        bool TrackTime(TimeLog timeLog);
    }
}
