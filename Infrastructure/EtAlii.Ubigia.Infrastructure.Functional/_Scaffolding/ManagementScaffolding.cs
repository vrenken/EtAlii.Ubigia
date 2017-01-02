namespace EtAlii.Ubigia.Infrastructure.Functional
{
    using SimpleInjector;

    internal class ManagementScaffolding : IScaffolding
    {
        public void Register(Container container)
        {
            container.Register<IStorageInitializer, StorageInitializer>(Lifestyle.Singleton);
            container.Register<ILocalStorageInitializer, LocalStorageInitializer>(Lifestyle.Singleton);

            container.Register<IStorageRepository, StorageRepository>(Lifestyle.Singleton);
            container.Register<IAccountRepository, AccountRepository>(Lifestyle.Singleton);
            container.Register<ISpaceRepository, SpaceRepository>(Lifestyle.Singleton);
        }
    }
}