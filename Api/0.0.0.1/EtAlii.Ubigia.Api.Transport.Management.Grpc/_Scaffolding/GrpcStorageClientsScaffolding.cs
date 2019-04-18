﻿namespace EtAlii.Ubigia.Api.Transport.Management.Grpc
{
    using EtAlii.xTechnology.MicroContainer;

    internal class GrpcStorageClientsScaffolding : IScaffolding
    {
        public void Register(Container container)
        {
            container.Register<IStorageConnection, GrpcStorageConnection>();

            //container.Register<IHubProxyMethodInvoker, HubProxyMethodInvoker>()

            container.Register<IAuthenticationManagementDataClient, GrpcAuthenticationManagementDataClient>();

            container.Register<IStorageDataClient, GrpcStorageDataClient>();
            container.Register<IAccountDataClient, GrpcAccountDataClient>();
            container.Register<ISpaceDataClient, GrpcSpaceDataClient>();

            // No Notification clients yet.
            container.Register<IStorageNotificationClient, StorageNotificationClientStub>();
            container.Register<IAccountNotificationClient, AccountNotificationClientStub>();
            container.Register<ISpaceNotificationClient, SpaceNotificationClientStub>();
        }
    }
}
