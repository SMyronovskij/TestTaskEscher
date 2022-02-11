using System;
using TestTaskEscher.DataModels.Dtos.PersonModels;
using TestTaskEscher.Providers;
using TestTaskEscher.Services.ConsoleServices;

namespace TestTaskEscher.Services
{
    public class UserInteractionService : IUserInteractionService
    {
        private readonly IConsoleService _consoleService;

        public UserInteractionService(IConsoleService consoleService)
        {
            _consoleService = consoleService;
        }

        #region PublicMethods
        
        public StorageType SelectStorage()
        {
            _consoleService.WriteLine("Please select one:");
            _consoleService.WriteLine("1 - Db storage");
            _consoleService.WriteLine("2 - Text file storage");

            var key = _consoleService.ReadKey();

            if (key.KeyChar == '1')
                return StorageType.Db;

            if (key.KeyChar == '2')
                return StorageType.File;

            return StorageType.Unknown;
        }

        public PersonDto CreateUser()
        {
            var person = ReadPersonMainData(PersonType.Person) as PersonDto;
            person.RegistrationAllowed = VerifyAllowedAge(person.DateOfBirth);
            if (!person.RegistrationAllowed) return person;

            _consoleService.WriteLine();

            var haveSpouse = ShowQuestionYn("Do you have spouse?");

            if (haveSpouse)
            {
                person.Spouse = ReadPersonMainData(PersonType.Spouse) as SpouseDto;
            }

            person.DateOfRegistration = DateTime.Now;

            return person;
        }

        public void PrintUser()
        {
            throw new NotImplementedException();
        }

        public void UserList()
        {
            throw new NotImplementedException();
        }

        public void SpouseList()
        {
            throw new NotImplementedException();
        }

        public MenuActions ShowMenu()
        {
            _consoleService.WriteLine();
            _consoleService.WriteLine("Please select one:");
            _consoleService.WriteLine("1 - Create user");
            //_consoleService.WriteLine("2 - Show users");
            _consoleService.WriteLine("9 - Exit");
            //Console.WriteLine("3 - Print spouses");

            var key = _consoleService.ReadKey();

            if (key.KeyChar == '1')
            {
                _consoleService.WriteLine();
                return MenuActions.CreateUser;

            }

            //if (key.KeyChar == '2')
            //    return MenuActions.ShowUsers;

            if (key.KeyChar == '9')
                return MenuActions.Exit;


            return MenuActions.Unknown;
        }

        public void ShowErrorBeforeExit()
        {
            _consoleService.WriteLine("Error executing app. Press any key to exit");
        }

        #endregion

        // Missing Delete && Update

        #region PrivateMethods

        private bool ShowQuestionYn(string message)
        {
            message += " [Y/N]: ";

            ConsoleKey response;
            do
            {
                _consoleService.Write(message);
                response = _consoleService.ReadKey(false).Key;   // true is intercept key (dont show), false is show

                if (response != ConsoleKey.Enter)
                    _consoleService.WriteLine();

            } while (response != ConsoleKey.Y && response != ConsoleKey.N);

            return response == ConsoleKey.Y;
        }
        private string GetUserTextInput(string message)
        {
            string input;
            bool invalidString;
            do
            {
                _consoleService.Write(message);
                input = _consoleService.ReadLine()!;
                invalidString = string.IsNullOrEmpty(input);

                if (!invalidString) invalidString = string.IsNullOrWhiteSpace(input);

                if (invalidString) _consoleService.WriteLine("Please enter valid text");

            } while (invalidString);

            return input;
        }
        private DateTime GetUserAge()
        {
            DateTime date = new DateTime();
            do
            {
                var input = GetUserTextInput("Please, enter your date of birth (e.g. mm/dd/yyyy)");

                DateTime.TryParse(input, out date);

                if (date == DateTime.MinValue) _consoleService.WriteLine("Error parsing date, please enter valid value");

            } while (date == DateTime.MinValue);

            return date;
        }

        private bool VerifyAllowedAge(DateTime dateOfBirth)
        {
            var yesNoMessage = "Parent authorization required.\nDo you allow to register your child?";
            var prohibitionMessage = "Registration prohibited";
            var allowedAge = DateTime.Now;
            allowedAge = allowedAge.AddYears(-18);

            if (dateOfBirth > allowedAge)
            {
                allowedAge = allowedAge.AddYears(2);
                if (dateOfBirth > allowedAge || !ShowQuestionYn(yesNoMessage))
                {
                    _consoleService.WriteLine(prohibitionMessage);
                    return false;
                }
            }
            return true;
        }

        private BasePersonDto ReadPersonMainData(PersonType personType)
        {
            BasePersonDto basePersonData = personType == PersonType.Person
                ? new PersonDto()
                : new SpouseDto();

            basePersonData.FirstName = GetUserTextInput("Enter FirstName: ");

            basePersonData.Surname = GetUserTextInput("Enter Surname: ");

            var dateOfBirth = GetUserAge();

            basePersonData.DateOfBirth = dateOfBirth;

            return basePersonData;
        }

        #endregion

        private enum PersonType
        {
            Person,
            Spouse
        }
    }

    public enum MenuActions
    {
        Unknown,
        CreateUser,
        ShowUsers,
        Exit = 9
    }
}