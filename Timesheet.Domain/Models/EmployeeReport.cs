using System;
using System.Collections.Generic;

namespace Timesheet.Domain.Models
{
    /// <summary>
    /// Отчет по сотруднику: [Имя сотрудника] за период за период с[дата начала] по[дата окончания]
    /// 10.10.2020, 8 часов, исправлял работу модуля отчетов
    /// 11.10.2020, 8 часов, разработка новой функциональности модуля интеграции
    /// 12.10.2020, 10 часов, срочные исправления модуля интеграции
    /// Итого: 26 часов, заработано: 2000 руб
    /// </summary>
    public class EmployeeReport
    {
        public EmployeeReport()
        {
            TimeLogs = new List<TimeLog>();
        }

        public string LastName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public List<TimeLog> TimeLogs { get; set; }

        public int TotalHours { get; set; }
        public decimal Bill { get; set; }
    }
}
