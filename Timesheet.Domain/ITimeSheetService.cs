using Timesheet.Domain.Models;

namespace Timesheet.Domain
{
    public interface ITimeSheetService
    {
        bool TrackTime(TimeLog timeLog);
    }
}
