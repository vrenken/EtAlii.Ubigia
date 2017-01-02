namespace EtAlii.Ubigia.Api.Management
{
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.xTechnology.MicroContainer;

    internal class StorageConnectionScaffolding : IScaffolding
    {
        private readonly IStorageConnectionConfiguration _configuration;

        public StorageConnectionScaffolding(IStorageConnectionConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Register(Container container)
        {
            container.Register<IStorageTransport>(() => _configuration.Transport);
            container.Register<IStorageConnectionConfiguration>(() => _configuration);

            container.Register<IAuthenticationContext, AuthenticationContext>();
            container.Register<IStorageContext, StorageContext>();
            container.Register<IAccountContext, AccountContext>();
            container.Register<ISpaceContext, SpaceContext>();
        }
    }
}
