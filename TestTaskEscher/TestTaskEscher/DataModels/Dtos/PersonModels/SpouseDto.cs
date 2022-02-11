using TestTaskEscher.DataModel;

namespace TestTaskEscher.DataModels.Dtos.PersonModels
{
    public class SpouseDto : BasePersonDto
    {
        public Spouse ToSpouse()
        {
            return new Spouse
            {
                FirstName = FirstName,
                Surname = Surname,
                DateOfBirth = DateOfBirth
            };
        }
    }
}
