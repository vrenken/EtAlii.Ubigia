// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Management.Grpc
{
    using EtAlii.xTechnology.MicroContainer;

    internal class GrpcStorageClientsScaffolding : IScaffolding
    {
        public void Register(IRegisterOnlyContainer container)
        {
            container.Register<IStorageConnection, GrpcStorageConnection>();

            //container.Register<IHubProxyMethodInvoker, HubProxyMethodInvoker>()

            container.Register<IAuthenticationManagementDataClient, GrpcAuthenticationManagementDataClient>();

            container.Register<IInformationDataClient, GrpcInformationDataClient>();
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
