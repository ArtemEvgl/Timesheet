using System;
using System.ComponentModel.DataAnnotations;

namespace Timesheet.DataAccess.MSSQL.Entities
{
    public class TimeLog
    {
        [Key]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int WorkingHours { get; set; }
        public string LastName { get; set; }
        public string Comment { get; set; }
    }
}
