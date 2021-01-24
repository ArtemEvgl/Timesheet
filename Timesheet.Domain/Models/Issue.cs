using System;
using System.Collections.Generic;
using System.Text;

namespace Timesheet.Domain.Models
{
    public class Issue
    {
        public int Id { get; set; }
        public int SourceId { get; set; }
        public string Name { get; set; }
    }
}
