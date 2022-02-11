using TestTaskEscher.DataModels.Dtos.PersonModels;
using TestTaskEscher.Providers;

namespace TestTaskEscher.Services
{
    public interface IUserInteractionService
    {
        StorageType SelectStorage();
        PersonDto CreateUser();
        void PrintUser();
        void UserList();
        void SpouseList();
        MenuActions ShowMenu();
        void ShowErrorBeforeExit();

    }
}
