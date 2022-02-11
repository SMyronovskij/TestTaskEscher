using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using Xunit;
using TestTaskEscher.DataModel;
using TestTaskEscher.DataModels.DbModels.PersonModels;
using TestTaskEscher.Providers.DataProviders;

namespace TestTaskEscher.Tests.Providers.DataProviders;

[ExcludeFromCodeCoverage]
public class TextFileDataProviderTest
{
    private IDataProvider _dataProvider;

    public TextFileDataProviderTest()
    {
        _dataProvider = new TextFileDataProvider();
    }

    [Fact]
    public async void CreatePersonTest()
    {
        var person = new Person
        {
            FirstName = "Test1",
            Surname = "Test2",
            DateOfBirth = new DateTime(1992, 11, 25),
            RegistrationAllowed = true
        };

        await _dataProvider.CreatePerson(person);

        var personLastLine = File.ReadLines(@"C:\people\mainFile.txt").Last();

        var data = personLastLine.Split("|");

        Assert.False(string.IsNullOrEmpty(data[0]));
        Assert.Equal(person.FirstName,data[1]);
        Assert.Equal(person.Surname,data[2]);
        Assert.Equal(person.DateOfBirth.ToString("d"),data[3]);
        Assert.True(System.Convert.ToBoolean(data[4]));
        Assert.Equal("Single", data[5]);
    }

    [Fact]
    public async void CreatePersonWithSpouseTest()
    {
        var person = new Person
        {
            FirstName = "Test1",
            Surname = "Test2",
            DateOfBirth = new DateTime(1992, 11, 25),
            RegistrationAllowed = true
        };
        var spouse = new Spouse
        {
            FirstName = "TestSpouse1",
            Surname = "TestSpouse2",
            DateOfBirth = new DateTime(1994, 5, 16)
        };

        await _dataProvider.CreatePerson(person, spouse);

        var personLastLine = File.ReadLines(@"C:\people\mainFile.txt").Last();
        var spouseLastLine = File.ReadLines(@"C:\people\spouses\spouses.txt").Last();

        var data = personLastLine.Split("|");

        Assert.False(string.IsNullOrEmpty(data[0]));
        Assert.Equal(person.FirstName, data[1]);
        Assert.Equal(person.Surname, data[2]);
        Assert.Equal(person.DateOfBirth.ToString("d"), data[3]);
        Assert.True(System.Convert.ToBoolean(data[4]));
        Assert.Equal("Married", data[5]);

        data = spouseLastLine.Split("|");

        Assert.False(string.IsNullOrEmpty(data[0]));
        Assert.Equal(spouse.FirstName, data[1]);
        Assert.Equal(spouse.Surname, data[2]);
        Assert.Equal(spouse.DateOfBirth.ToString("d"), data[3]);
    }
}