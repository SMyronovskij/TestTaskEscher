using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestTaskEscher.DataModel;
using TestTaskEscher.DataModels.DbModels.PersonModels;

namespace TestTaskEscher.Providers.DataProviders
{
    internal class DBProvider : IDataProvider
    {
        public Task CreatePerson(Person person, Spouse spouse = null)
        {
            throw new NotImplementedException();
        }

        public Person GetPerson(int Id)
        {
            throw new NotImplementedException();
        }

        public List<BasePerson> GetPeople()
        {
            throw new NotImplementedException();
        }

        public Spouse GetSpouse(int Id)
        {
            throw new NotImplementedException();
        }

        public List<Spouse> GetSpouses()
        {
            throw new NotImplementedException();
        }
    }
}
