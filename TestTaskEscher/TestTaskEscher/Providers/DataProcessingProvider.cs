using System;
using System.Collections.Generic;
using TestTaskEscher.DataModels.Dtos.PersonModels;
using TestTaskEscher.Providers.DataProviders;

namespace TestTaskEscher.Providers
{
    public class DataProcessingProvider : IDataProcessingProvider
    {
        private readonly IDataProvider _dataProvider;
        private StorageType _currentStorage;

        public DataProcessingProvider(IDataProvider dataProvider)
        {
            _dataProvider = dataProvider;
        }

        public void SetStorageType(StorageType storageType)
        {
            _currentStorage = storageType;
        }

        public void CreatePerson(PersonDto person)
        {
            var DbPerson = person.ToPerson();
            if (person.Spouse != null)
            {
                var DbSpouse = person.Spouse.ToSpouse();
                _dataProvider.CreatePerson(DbPerson, DbSpouse);
            }
            else
            {
                _dataProvider.CreatePerson(DbPerson);
            }
        }

        public PersonDto GetPerson(int Id)
        {
            throw new NotImplementedException();
        }

        public List<BasePersonDto> GetPeople()
        {
            throw new NotImplementedException();
        }

        public SpouseDto GetSpouse(int Id)
        {
            throw new NotImplementedException();
        }

        public List<SpouseDto> GetSpouses()
        {
            throw new NotImplementedException();
        }
    }

    public enum StorageType
    {
        Unknown,
        File,
        Db
    }
}