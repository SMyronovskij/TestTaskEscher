namespace TestTaskEscher.DataModels.Dtos.PersonModels;

public class BasePersonDto
{
    public int Id { get; set; }

    public string FirstName { get; set; }

    public string Surname { get; set; }

    public DateTime DateOfBirth { get; set; }
}