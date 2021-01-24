using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Timesheet.Api.Models;
using Timesheet.Domain;
using Timesheet.Domain.Models;

namespace Timesheet.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimesheetController : Controller
    {
        private readonly ITimeSheetService _timeSheetService;
        public TimesheetController(ITimeSheetService timeSheetService)
        {
            _timeSheetService = timeSheetService;
        }

        [HttpPost]
        public ActionResult<bool> TrackTime(CreateTimeLogRequest request)
        {
            var lastName = (string)HttpContext.Items["LastName"];

            if (ModelState.IsValid)
            {
                var timeLog = new TimeLog
                {
                    Comment = request.Comment,
                    Date = request.Date,
                    LastName = request.LastName,
                    WorkingHours = request.WorkingHours
                };

                var result = _timeSheetService.TrackTime(timeLog, lastName);
                return Ok(result);
            }

            return BadRequest();
        }
    }
}
