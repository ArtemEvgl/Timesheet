using NUnit.Framework;
using Timesheet.Api.Services;

namespace Timesheet.Tests
{
    class ReportServiceTests
    {
        [Test]
        public void GetEmployeeReport_ShouldReturnReport()
        {
            //arrange
            var service = new ReportService();

            var expectedLastName = "Иванов";
            var expectedTotal = 100m;

            //act
            var result = service.GetEmployeeReport("Иванов");

            //assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedLastName, result.LastName);

            Assert.IsNotNull(result.TimeLogs);
            Assert.IsNotEmpty(result.TimeLogs);

            Assert.AreEqual(expectedTotal, result.Bill);
        }
    }
}
