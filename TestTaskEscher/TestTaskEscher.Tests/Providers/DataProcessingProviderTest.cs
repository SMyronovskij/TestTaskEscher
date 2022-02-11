using System;
using Moq;
using TestTaskEscher.DataModel;
using TestTaskEscher.DataModels.DbModels.PersonModels;
using TestTaskEscher.DataModels.Dtos.PersonModels;
using TestTaskEscher.Providers;
using TestTaskEscher.Providers.DataProviders;
using Xunit;

namespace TestTaskEscher.Tests.Providers
{
    public class DataProcessingProviderTest
    {
        private Mock<IDataProvider> _dataProviderMock;
        private IDataProcessingProvider _dataProcessingProvider;

        public DataProcessingProviderTest()
        {
            _dataProviderMock = new Mock<IDataProvider>();
            _dataProcessingProvider = new DataProcessingProvider(_dataProviderMock.Object);
        }

        [Fact]
        public void CreatePersonTest()
        {
            var person = new PersonDto
            {
                FirstName = "TestName",
                Surname = "TestSurname",
                DateOfBirth = new DateTime(1992, 2, 1),
            };

            _dataProcessingProvider.CreatePerson(person);

            _dataProviderMock.Verify(x =>
                x.CreatePerson(It.IsNotNull<Person>(), null), Times.Once);
        }

        [Fact]
        public void CreatePersonWithSpouseTest()
        {
            var person = new PersonDto
            {
                FirstName = "TestName",
                Surname = "TestSurname",
                DateOfBirth = new DateTime(1992, 1, 2),
                Spouse = new SpouseDto
                {
                    FirstName = "TestSpouse1",
                    Surname = "TestSpouse2",
                    DateOfBirth = new DateTime(2000, 2, 3)
                }
            };

            _dataProcessingProvider.CreatePerson(person);

            _dataProviderMock.Verify(x =>
                x.CreatePerson(It.IsNotNull<Person>(), It.IsNotNull<Spouse>()), Times.Once);
        }
    }
}