// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Transport
{
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Infrastructure.Functional;
    using EtAlii.xTechnology.MicroContainer;

    internal class SystemClientsScaffolding : IScaffolding
    {
        private readonly IInfrastructure _infrastructure;
        private readonly SpaceConnectionOptions _options;

        public SystemClientsScaffolding(IInfrastructure infrastructure, SpaceConnectionOptions options = null)
        {
            _infrastructure = infrastructure;
            _options = options;
        }

        public void Register(IRegisterOnlyContainer container)
        {
            if (_options != null)
            {
                container.Register<ISpaceConnection>(serviceCollection =>
                {
                    var transport = serviceCollection.GetInstance<ISpaceTransport>();
                    var roots = serviceCollection.GetInstance<IRootContext>();
                    var entries = serviceCollection.GetInstance<IEntryContext>();
                    var content = serviceCollection.GetInstance<IContentContext>();
                    var properties = serviceCollection.GetInstance<IPropertiesContext>();
                    var authentication = serviceCollection.GetInstance<IAuthenticationContext>();
                    return new SystemSpaceConnection(transport, _options, roots, entries, content, properties, authentication);
                });
            }
            container.Register<IStorageConnection, SystemStorageConnection>();

            container.Register(() => _infrastructure);

            // Data clients.
            container.Register<IAuthenticationDataClient, SystemAuthenticationDataClient>();

            container.Register<IInformationDataClient, SystemInformationDataClient>();

            container.Register<IEntryDataClient, SystemEntryDataClient>();
            container.Register<IRootDataClient, SystemRootDataClient>();
            container.Register<IPropertiesDataClient, SystemPropertiesDataClient>();
            container.Register<IContentDataClient, SystemContentDataClient>();

            // Only management data clients as we do not have any management notification clients (yet).
            container.Register<IAuthenticationManagementDataClient, SystemAuthenticationManagementDataClient>();

            container.Register<IStorageDataClient, SystemStorageDataClient>();
            container.Register<IAccountDataClient, SystemAccountDataClient>();
            container.Register<ISpaceDataClient, SystemSpaceDataClient>();
        }
    }
}
