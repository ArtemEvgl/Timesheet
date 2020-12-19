using Moq;
using NUnit.Framework;
using Timesheet.Application.Services;
using Timesheet.Domain.Models;
using Timesheet.Domain;
using System;

namespace Timesheet.Tests
{
    class ReportServiceTests
    {
        [Test]
        public void GetEmployeeReport_ShouldReturnReportPerOneMonth()
        {
            //arrange
            var timesheetRepositoryMock = new Mock<ITimesheetRepository>();
            var employeeRepositoryMock = new Mock<IEmployeeRepository>();
            var expectedLastName = "Иванов";
            var expectedTotal = 8750m; // (8+8+4)/160 * 70000

            timesheetRepositoryMock
                .Setup(x => x.GetTimeLogs(It.Is<string>(y => y == expectedLastName)))
                .Returns(() => new[] 
                {
                    new TimeLog
                    {
                        LastName = expectedLastName,
                        Date = DateTime.Now.AddDays(-2),
                        WorkingHours = 8,
                        Comment = Guid.NewGuid().ToString()
                    },
                    new TimeLog
                    {
                        LastName = expectedLastName,
                        Date = DateTime.Now.AddDays(-1),
                        WorkingHours = 8,
                        Comment = Guid.NewGuid().ToString()
                    },
                    new TimeLog
                    {
                        LastName = expectedLastName,
                        Date = DateTime.Now,
                        WorkingHours = 4,
                        Comment = Guid.NewGuid().ToString()
                    }
                })
                .Verifiable();

            employeeRepositoryMock
                .Setup(x => x.GetEmployee(It.Is<string>(y => y == expectedLastName)))
                .Returns(() => new StaffEmployee
                {
                    LastName = expectedLastName,
                    Salary = 70000
                })
                .Verifiable();

            var service = new ReportService(timesheetRepositoryMock.Object, employeeRepositoryMock.Object);

            //act
            var result = service.GetEmployeeReport(expectedLastName);

            //assert
            timesheetRepositoryMock.VerifyAll();

            Assert.IsNotNull(result);
            Assert.AreEqual(expectedLastName, result.LastName);

            Assert.IsNotNull(result.TimeLogs);
            Assert.IsNotEmpty(result.TimeLogs);

            Assert.AreEqual(expectedTotal, result.Bill);
        }

        [Test]
        public void GetEmployeeReport_ShouldReturnReportPerSeveralMonth()
        {
            //arrange
            var timesheetRepositoryMock = new Mock<ITimesheetRepository>();
            var employeeRepositoryMock = new Mock<IEmployeeRepository>();
            var expectedLastName = "Иванов";
            // ставка за час = 60000 / 160 = 375
            //  20 * 8 * 375 + 10 * 8 * 375 * 2(дни переработки) + 1 * 375 * 2(1 час переработки в 1 дне) + 5 * 8 * 375 (рабочие дни в след месяце)
            var expectedTotal = 135750m; 
            

            employeeRepositoryMock
                .Setup(x => x.GetEmployee(It.Is<string>(x => x == expectedLastName)))
                .Returns(() => new StaffEmployee
                {
                    LastName = expectedLastName,
                    Salary = 60000
                })
                .Verifiable();

            timesheetRepositoryMock
                .Setup(x => x.GetTimeLogs(It.Is<string>(x => x == expectedLastName)))
                .Returns(() =>
                {
                    TimeLog[] timeLogs = new TimeLog[35]; 
                    DateTime dateTime = new DateTime(2020, 11, 1);
                    timeLogs[0] = new TimeLog
                    {
                        LastName = expectedLastName,
                        Comment = Guid.NewGuid().ToString(),
                        Date = dateTime,
                        WorkingHours = 9
                    };
                    for (int i = 1; i < timeLogs.Length; i++)
                    {
                        dateTime = dateTime.AddDays(1);
                        timeLogs[i] = new TimeLog { 
                            LastName = expectedLastName, 
                            Comment = Guid.NewGuid().ToString(), 
                            Date = dateTime,
                            WorkingHours = 8
                        };                        
                    }
                    return timeLogs;
                })
                .Verifiable();

            var service = new ReportService(timesheetRepositoryMock.Object, employeeRepositoryMock.Object);
            //act
            var result = service.GetEmployeeReport(expectedLastName);
            //assert

            Assert.IsNotNull(result);

            Assert.AreEqual(expectedLastName, result.LastName);
            Assert.IsNotNull(result.TimeLogs);
            Assert.IsNotEmpty(result.TimeLogs);

            Assert.AreEqual(expectedTotal, result.Bill);
        }
    }
}
