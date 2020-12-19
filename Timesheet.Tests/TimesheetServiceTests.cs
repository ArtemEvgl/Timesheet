using NUnit.Framework;
using System;
using Timesheet.Application.Services;
using Timesheet.Domain.Models;
using static Timesheet.Application.Services.AuthService;

namespace Timesheet.Tests
{
    class TimesheetServiceTests
    {
        [Test]
        public void TrackTime_ShouldReturnTrue()
        {
            //arrange
            var expectedLastName = "TestUser";

            UserSessions.Sessions.Add(expectedLastName);

            var timeLog = new TimeLog
            {
                Date = new DateTime(),
                WorkingHours = 1,
                LastName = expectedLastName,
                Comment = Guid.NewGuid().ToString()
            };

            var service = new TimesheetService();

            //act
            var result = service.TrackTime(timeLog);

            //assert
            Assert.IsTrue(result);
        }

        // WorkingHours = 25, - should return false

        [TestCase(25, "")]
        [TestCase(25, null)]
        [TestCase(25, "TestUser")]
        [TestCase(-1, "")]
        [TestCase(-1, null)]
        [TestCase(-1, "TestUser")]
        [TestCase(4, "")]
        [TestCase(4, null)]
        [TestCase(4, "TestUser")]
        public void TrackTime_ShouldReturnFalse(int hours, string lastName)
        {
            //arrange

            var timeLog = new TimeLog
            {
                Date = new DateTime(),
                WorkingHours = hours,
                LastName = lastName,
                Comment = Guid.NewGuid().ToString()
            };

            var service = new TimesheetService();
            //act
            var result = service.TrackTime(timeLog);

            //assert
            Assert.IsFalse(result);
        }
    }
}
