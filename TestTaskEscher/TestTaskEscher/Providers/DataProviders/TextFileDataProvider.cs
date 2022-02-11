using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using TestTaskEscher.DataModel;
using TestTaskEscher.DataModels.DbModels.PersonModels;

namespace TestTaskEscher.Providers.DataProviders
{
    public class TextFileDataProvider : IDataProvider
    {
        private readonly string root = @"C:\people";
        private readonly string mainFileName = @"\mainFile.txt";
        private readonly string spousesRoot = @"\spouses";
        private readonly string spousesFileName = @"\spouses.txt";


        public TextFileDataProvider()
        {
            CreateDirectoryIfEmpty();
            CreateFileIfEmpty();
        }

        private void CreateDirectoryIfEmpty()
        {
            if (Directory.Exists(root))
            {
                if (!Directory.Exists(Path.Combine(root + spousesRoot)))
                {
                    Directory.CreateDirectory(Path.Combine(root + spousesRoot));
                }
            }
            else
            {
                Directory.CreateDirectory(root);
                Directory.CreateDirectory(Path.Combine(root + spousesRoot));
            }
        }

        private void CreateFileIfEmpty()
        {
            if (!File.Exists(Path.Combine(root + mainFileName)))
            {
                var stream = File.Create(root + mainFileName);
                stream.Close();
            }

            if (!File.Exists(Path.Combine(root + spousesRoot + spousesFileName)))
            {
                var stream = File.Create(root + spousesRoot + spousesFileName);
                stream.Close();
            }
        }

        public async Task CreatePerson(Person person, Spouse spouse = null)
        {
            var id = (int)DateTimeOffset.Now.ToUnixTimeSeconds();
            person.Id = id;
            if (spouse != null)
            {
                person.SpouseId = id;
                spouse.Id = id;
            }

            await using (StreamWriter file = new(root + mainFileName, append: true))
            {
                await file.WriteLineAsync(person.ToString());
            }

            if (spouse != null)
            {
                await using (StreamWriter file = new(root + spousesRoot + spousesFileName, append: true))
                {
                    await file.WriteLineAsync(spouse.ToString());
                }
            }

            await Task.Delay(1000); //Due to id generation to prevent same id per record
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