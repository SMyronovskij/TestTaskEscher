using TestTaskEscher.Providers;
using TestTaskEscher.Services;

#region Init

var appOperationService = new AppOperationService(IocProvider.InteractionService);

#endregion

appOperationService.StartUserFlow();