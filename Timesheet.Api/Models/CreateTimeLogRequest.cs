using FluentValidation;
using System;

namespace Timesheet.Api.Models
{
    public class CreateTimeLogRequest
    {
        public DateTime Date { get; set; }
        public int WorkingHours { get; set; }
        public string LastName { get; set; }
        public string Comment { get; set; }
    }

    public class TimeLogFluentValidator : AbstractValidator<CreateTimeLogRequest>
    {
        public TimeLogFluentValidator()
        {
            RuleFor(x => x.Date)
                .GreaterThan(DateTime.Now.AddYears(-1))
                .LessThan(DateTime.Now);

            RuleFor(x => x.WorkingHours)
                .GreaterThan(0)
                .LessThanOrEqualTo(24);

            RuleFor(x => x.LastName)
                .NotEmpty();
        }
    }
}
