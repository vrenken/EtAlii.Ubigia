﻿namespace EtAlii.Servus.Api.Management.WebApi
{
    using EtAlii.Servus.Api.Transport;
    using EtAlii.Servus.Api.Transport.WebApi;
    using EtAlii.xTechnology.MicroContainer;

    internal class WebApiStorageClientsScaffolding : IScaffolding
    {
        private readonly IInfrastructureClient _infrastructureClient;

        public WebApiStorageClientsScaffolding(IInfrastructureClient infrastructureClient)
        {
            _infrastructureClient = infrastructureClient;
        }

        public void Register(Container container)
        {
            container.Register<IAddressFactory, AddressFactory>();
            container.Register<IStorageConnection, WebApiStorageConnection>();

            container.Register<IAuthenticationDataClient, WebApiAuthenticationDataClient>();

            container.Register<IStorageDataClient, WebApiStorageDataClient>();
            container.Register<IAccountDataClient, WebApiAccountDataClient>();
            container.Register<ISpaceDataClient, WebApiSpaceDataClient>();

            // No Notification clients yet.
            container.Register<IStorageNotificationClient, StorageNotificationClientStub>();
            container.Register<IAccountNotificationClient, AccountNotificationClientStub>();
            container.Register<ISpaceNotificationClient, SpaceNotificationClientStub>();


            if (_infrastructureClient != null)
            {
                container.Register<IInfrastructureClient>(() => _infrastructureClient);
            }
            else
            {
                container.Register<IInfrastructureClient, DefaultInfrastructureClient>();
                container.Register<ISerializer>(() => new SerializerFactory().Create());
                container.Register<IHttpClientFactory, DefaultHttpClientFactory>();
            }

        }
    }
}
