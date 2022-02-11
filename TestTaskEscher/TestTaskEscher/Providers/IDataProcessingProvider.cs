using System.Collections.Generic;
using TestTaskEscher.DataModels.Dtos.PersonModels;

namespace TestTaskEscher.Providers
{
    public interface IDataProcessingProvider
    {
        void SetStorageType(StorageType storageType);
        void CreatePerson(PersonDto person);
        PersonDto GetPerson(int Id);
        List<BasePersonDto> GetPeople();
        SpouseDto GetSpouse(int Id);
        List<SpouseDto> GetSpouses();
    }
}