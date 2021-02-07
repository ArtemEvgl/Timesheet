using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Timesheet.Domain;
using Timesheet.Domain.Models;

namespace Timesheet.DataAccess.MSSQL.Repositories
{
    public class TimesheetRepository : ITimesheetRepository
    {
        private readonly TimesheetContext _context;
        private readonly IMapper _mapper;

        public TimesheetRepository(TimesheetContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Add(TimeLog timeLog)
        {
            var timeLogEntity = _mapper.Map<Entities.TimeLog>(timeLog);
            _context.TimeLogs.Add(timeLogEntity);
            _context.SaveChanges();
        }

        public void Update(TimeLog timeLog)
        {
            var updatedTimeLog = _mapper.Map<Entities.TimeLog>(timeLog);

            _context.TimeLogs.Update(updatedTimeLog);
            _context.SaveChanges();
        }

        public TimeLog Get(int timeLogId)
        {
            var timeLog = _context.TimeLogs
                .AsNoTracking()
                .FirstOrDefault(x => x.Id == timeLogId);

            return _mapper.Map<TimeLog>(timeLog);
        }

        public TimeLog[] GetTimeLogs(string lastName)
        {
            var timeLogs = _context.TimeLogs
                .AsNoTracking()
                .Where(x => x.LastName == lastName)
                .ToArray();

            return _mapper.Map<TimeLog[]>(timeLogs);
        }
    }
}
