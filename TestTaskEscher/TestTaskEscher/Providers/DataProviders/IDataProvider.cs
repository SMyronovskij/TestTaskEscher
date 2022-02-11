using System.Collections.Generic;
using System.Threading.Tasks;
using TestTaskEscher.DataModel;
using TestTaskEscher.DataModels.DbModels.PersonModels;

namespace TestTaskEscher.Providers.DataProviders
{
    public interface IDataProvider
    {
        Task CreatePerson(Person person, Spouse spouse = null);
        Person GetPerson(int Id);
        List<BasePerson> GetPeople();
        Spouse GetSpouse(int Id);
        List<Spouse> GetSpouses();
    }
}