using System;
using TestTaskEscher.DataModels.DbModels.PersonModels;

namespace TestTaskEscher.DataModels.Dtos.PersonModels
{
    public class PersonDto : BasePersonDto
    {
        public DateTime DateOfRegistration { get; set; }

        public bool RegistrationAllowed { get; set; }

        public SpouseDto Spouse { get; set; }

        public Person ToPerson()
        {
            return new Person
            {
                FirstName = FirstName,
                Surname = Surname,
                DateOfBirth = DateOfBirth,
                DateOfRegistration = DateOfRegistration,
                RegistrationAllowed = RegistrationAllowed,
            };
        }
    }
}