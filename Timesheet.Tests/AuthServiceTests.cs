using NUnit.Framework;
using Timesheet.Application.Services;
using static Timesheet.Application.Services.AuthService;

namespace Timesheet.Tests
{
    public class AuthServiceTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [TestCase("Иванов")]
        [TestCase("Петров")]
        [TestCase("Сидоров")]
        public void Login_ShouldReturnTrue(string lastName)
        {
            //arrange
            var service = new AuthService();

            //act

            var result = service.Login(lastName);

            //assert
            Assert.IsNotNull(UserSessions.Sessions);
            Assert.IsNotEmpty(UserSessions.Sessions);
            Assert.IsTrue(UserSessions.Sessions.Contains(lastName));
            Assert.IsTrue(result);
        }

        
        public void Login_InvokeLoginTwiceForOneLastName_ShouldReturnTrue()
        {
            //arrange
            string lastName = "Ñèäîðîâ";
            var service = new AuthService();

            //act

            var result = service.Login(lastName);
            result = service.Login(lastName);

            //assert
            Assert.IsNotNull(UserSessions.Sessions);
            Assert.IsNotEmpty(UserSessions.Sessions);
            Assert.IsTrue(UserSessions.Sessions.Contains(lastName));
            Assert.IsTrue(result);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("TestUser")]
        public void Login_ShouldReturnFalse(string lastName)
        {
            //arrange
            var service = new AuthService();

            //act

            var result = service.Login(lastName);

            //assert
            Assert.IsFalse(result);
        }
    }
}