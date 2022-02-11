using System;
using System.Diagnostics.CodeAnalysis;
using Moq;
using TestTaskEscher.Providers.DataProviders;
using TestTaskEscher.Services;
using TestTaskEscher.Services.ConsoleServices;
using Xunit;

namespace TestTaskEscher.Tests.Services
{
    [ExcludeFromCodeCoverage]
    public class UserInteractionServiceTest
    {
        private Mock<IConsoleService> _consoleService;
        private IUserInteractionService _interactionService;

        public UserInteractionServiceTest()
        {
            _consoleService = new Mock<IConsoleService>();
            _interactionService = new UserInteractionService(_consoleService.Object);
        }

        [Fact]
        public void CreateUserWithSpouseTest()
        {
            var userName = "TestName";
            var userSurname = "TestSurname";
            var userDateOfBirthString = "1.1.1992";
            var spouseName = "SpouseName";
            var spouseSurname = "SpouseSurname";
            var spouseDateOfBirthString = "2.2.1993";

            _consoleService.SetupSequence(x => x.ReadLine())
                .Returns(userName)
                .Returns(userSurname)
                .Returns(userDateOfBirthString)
                .Returns(spouseName)
                .Returns(spouseSurname)
                .Returns(spouseDateOfBirthString);

            _consoleService.Setup(x => x.ReadKey(false))
                .Returns(new ConsoleKeyInfo('y',ConsoleKey.Y,false,false,false));

            var user = _interactionService.CreateUser();

            DateTime.TryParse(userDateOfBirthString, out DateTime userDateOfBirth);
            DateTime.TryParse(spouseDateOfBirthString, out DateTime spouseDateOfBirth);

            Assert.Equal(userName,user.FirstName);
            Assert.Equal(userSurname, user.Surname);
            Assert.Equal(userDateOfBirth, user.DateOfBirth);
            Assert.Equal(spouseName, user.Spouse.FirstName);
            Assert.Equal(spouseSurname, user.Spouse.Surname);
            Assert.Equal(spouseDateOfBirth, user.Spouse.DateOfBirth);
        }

        [Fact]
        public void CreateUserWithoutSpouseTest()
        {
            var userName = "TestName";
            var userSurname = "TestSurname";
            var userDateOfBirthString = "1.1.1992";

            _consoleService.SetupSequence(x => x.ReadLine())
                .Returns(userName)
                .Returns(userSurname)
                .Returns(userDateOfBirthString);

            _consoleService.Setup(x => x.ReadKey(false))
                .Returns(new ConsoleKeyInfo('n', ConsoleKey.N, false, false, false));

            var user = _interactionService.CreateUser();

            DateTime.TryParse(userDateOfBirthString, out DateTime userDateOfBirth);

            Assert.Equal(userName, user.FirstName);
            Assert.Equal(userSurname, user.Surname);
            Assert.Equal(userDateOfBirth, user.DateOfBirth);
        }

        [Fact]
        public void CreateUserUnderAgeTest()
        {
            var userName = "TestName";
            var userSurname = "TestSurname";
            var userDateOfBirthString = "1.1.2012";

            _consoleService.SetupSequence(x => x.ReadLine())
                .Returns(userName)
                .Returns(userSurname)
                .Returns(userDateOfBirthString);

            var user = _interactionService.CreateUser();

            Assert.False(user.RegistrationAllowed);
        }

        [Fact]
        public void CreateUserParentsRestrictedTest()
        {
            var userName = "TestName";
            var userSurname = "TestSurname";
            var userDateOfBirthString = "1.1.2006";

            _consoleService.SetupSequence(x => x.ReadLine())
                .Returns(userName)
                .Returns(userSurname)
                .Returns(userDateOfBirthString);

            _consoleService.Setup(x => x.ReadKey(false))
                .Returns(new ConsoleKeyInfo('n', ConsoleKey.N, false, false, false));

            var user = _interactionService.CreateUser();

            Assert.False(user.RegistrationAllowed);
        }

        [Fact]
        public void CreateUserParentsAllowedTest()
        {
            var userName = "TestName";
            var userSurname = "TestSurname";
            var userDateOfBirthString = "1.1.2006";

            _consoleService.SetupSequence(x => x.ReadLine())
                .Returns(userName)
                .Returns(userSurname)
                .Returns(userDateOfBirthString);

            _consoleService.SetupSequence(x => x.ReadKey(false))
                .Returns(new ConsoleKeyInfo('y', ConsoleKey.Y, false, false, false))
                .Returns(new ConsoleKeyInfo('n', ConsoleKey.N, false, false, false));


            var user = _interactionService.CreateUser();

            Assert.True(user.RegistrationAllowed);
        }

    }
}