namespace EtAlii.Ubigia.Infrastructure.Functional
{
    using EtAlii.xTechnology.MicroContainer;

    internal class ManagementScaffolding : IScaffolding
    {
        public void Register(Container container)
        {
            container.Register<IStorageInitializer, StorageInitializer>();
            container.Register<ILocalStorageInitializer, LocalStorageInitializer>();

            container.Register<IStorageRepository, StorageRepository>();
            container.Register<IAccountRepository, AccountRepository>();
            container.Register<ISpaceRepository, SpaceRepository>();
        }
    }
}