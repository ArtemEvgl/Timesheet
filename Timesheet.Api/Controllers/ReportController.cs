using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Timesheet.Api.Models;
using Timesheet.Domain;

namespace Timesheet.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;
        private readonly IMapper _mapper;

        public ReportController(IReportService reportService, IMapper mapper)
        {
            _reportService = reportService;
            _mapper = mapper;
        }
        
        [HttpGet]
        public ActionResult<GetEmployeeReportResponse> Get(string lastName)
        {
            var result = _reportService.GetEmployeeReport(lastName);

            return _mapper.Map<GetEmployeeReportResponse>(result);
        }
    }
}
