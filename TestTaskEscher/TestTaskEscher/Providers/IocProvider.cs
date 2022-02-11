using TestTaskEscher.Providers.DataProviders;
using TestTaskEscher.Services;
using TestTaskEscher.Services.ConsoleServices;

namespace TestTaskEscher.Providers
{
    public static class IocProvider
    {
        public static IDataProcessingProvider InitDataProcessingProvider(StorageType storageType)
        {
            switch (storageType)
            {
                case StorageType.File:
                    DataProcessingProvider = new DataProcessingProvider(new TextFileDataProvider());
                    break;
                case StorageType.Db:
                    DataProcessingProvider = new DataProcessingProvider(new DBProvider());
                    break;
            }

            return DataProcessingProvider;
        }

        public static IDataProcessingProvider DataProcessingProvider;

        public static IUserInteractionService InteractionService 
            = new UserInteractionService(new ConsoleService());

    }
}
