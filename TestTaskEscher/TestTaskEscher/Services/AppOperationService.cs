using TestTaskEscher.Providers;

namespace TestTaskEscher.Services;

public class AppOperationService
{
    private readonly IUserInteractionService _interactionService;
    private IDataProcessingProvider _dataProcessingProvider;

    public AppOperationService(IUserInteractionService interactionService)
    {
        _interactionService = interactionService;
    }

    public void StartUserFlow()
    {
        var success = SelectDataStorage();

        if (!success)
        {
            _interactionService.ShowErrorBeforeExit();
            return;
        }

        MenuActions result;
        do
        {
            result = ShowMenu();

            switch (result)
            {
                case MenuActions.CreateUser:
                    CreateUser();
                    break;
                case MenuActions.ShowUsers:
                    break;
                case MenuActions.Exit:
                    break;
            }
        } while (result == MenuActions.Unknown || result != MenuActions.Exit);
    }

    private void CreateUser()
    {
        var person = _interactionService.CreateUser();
        if (!person.RegistrationAllowed) return;
        _dataProcessingProvider.CreatePerson(person);
    }


    private bool SelectDataStorage()
    {
        /*var storageType = _interactionService.SelectStorage();

        if (storageType == StorageType.Unknown)
            return false;*/

        _dataProcessingProvider = IocProvider.InitDataProcessingProvider(StorageType.File);
        return true;
    }

    private MenuActions ShowMenu()
    {
        MenuActions userAction;
        do
        {
            userAction = _interactionService.ShowMenu();
        } while (userAction == MenuActions.Unknown);

        return userAction;
    }
}