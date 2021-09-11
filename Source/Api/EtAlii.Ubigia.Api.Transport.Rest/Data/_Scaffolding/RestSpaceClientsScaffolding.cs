// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Rest
{
    using EtAlii.Ubigia.Serialization;
    using EtAlii.xTechnology.MicroContainer;

    internal class RestSpaceClientsScaffolding : IScaffolding
    {
        private readonly IRestInfrastructureClient _infrastructureClient;
        private readonly SpaceConnectionOptions _options;

        public RestSpaceClientsScaffolding(IRestInfrastructureClient infrastructureClient, SpaceConnectionOptions options)
        {
            _infrastructureClient = infrastructureClient;
            _options = options;
        }

        public void Register(IRegisterOnlyContainer container)
        {
            container.Register<IAddressFactory, AddressFactory>();
            container.Register<ISpaceConnection>(serviceCollection =>
            {
                var transport = serviceCollection.GetInstance<ISpaceTransport>();
                var roots = serviceCollection.GetInstance<IRootContext>();
                var entries = serviceCollection.GetInstance<IEntryContext>();
                var content = serviceCollection.GetInstance<IContentContext>();
                var properties = serviceCollection.GetInstance<IPropertiesContext>();
                var authentication = serviceCollection.GetInstance<IAuthenticationContext>();
                var addressFactory = serviceCollection.GetInstance<IAddressFactory>();
                var client = serviceCollection.GetInstance<IRestInfrastructureClient>();
                return new RestSpaceConnection(transport, addressFactory, client, _options, roots, entries, content, properties, authentication);
            });

            container.Register<IAuthenticationDataClient, RestAuthenticationDataClient>();

            container.Register<IEntryDataClient, RestEntryDataClient>();
            container.Register<IRootDataClient, RestRootDataClient>();
            container.Register<IContentDataClient, RestContentDataClient>();
            container.Register<IPropertiesDataClient, RestPropertiesDataClient>();

            // Web API does not support notifications (yet).
            container.Register<IEntryNotificationClient, EntryNotificationClientStub>();
            container.Register<IRootNotificationClient, RootNotificationClientStub>();
            container.Register<IContentNotificationClient, ContentNotificationClientStub>();
            container.Register<IPropertiesNotificationClient, PropertiesNotificationClientStub>();

            if (_infrastructureClient != null)
            {
                container.Register(() => _infrastructureClient);
            }
            else
            {
                container.Register<IRestInfrastructureClient, RestInfrastructureClient>();
                container.Register(() => new SerializerFactory().Create());
                container.Register<IHttpClientFactory, RestHttpClientFactory>();
            }
        }
    }
}
