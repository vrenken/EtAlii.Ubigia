// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Rest
{
    using EtAlii.Ubigia.Serialization;
    using EtAlii.xTechnology.MicroContainer;

    internal class RestSpaceClientsScaffolding : IScaffolding
    {
        private readonly IRestInfrastructureClient _infrastructureClient;

        public RestSpaceClientsScaffolding(IRestInfrastructureClient infrastructureClient)
        {
            _infrastructureClient = infrastructureClient;
        }

        public void Register(Container container)
        {
            container.Register<IAddressFactory, AddressFactory>();
            container.Register<ISpaceConnection, RestSpaceConnection>();

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
                container.Register<IHttpClientFactory, DefaultHttpClientFactory>();
            }
        }
    }
}
