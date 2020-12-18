using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Timesheet.Api.Models;
using Timesheet.Api.Services;

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
        public ActionResult<bool> TrackTime(TimeLog timeLog)
        {
            return Ok(_timeSheetService.TrackTime(timeLog));
        }
    }
}
