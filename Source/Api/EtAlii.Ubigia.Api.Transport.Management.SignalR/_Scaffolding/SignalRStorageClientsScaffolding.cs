// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Management.SignalR
{
    using EtAlii.Ubigia.Api.Transport.SignalR;
    using EtAlii.xTechnology.MicroContainer;

    internal class SignalRStorageClientsScaffolding : IScaffolding
    {
        public void Register(Container container)
        {
            container.Register<IStorageConnection, SignalRStorageConnection>();

            container.Register<IHubProxyMethodInvoker, HubProxyMethodInvoker>();

            container.Register<IAuthenticationManagementDataClient, SignalRAuthenticationManagementDataClient>();
            container.Register<ISignalRAuthenticationTokenGetter, SignalRAuthenticationTokenGetter>();

            container.Register<IInformationDataClient, SignalRInformationDataClient>();
            container.Register<IStorageDataClient, SignalRStorageDataClient>();
            container.Register<IAccountDataClient, SignalRAccountDataClient>();
            container.Register<ISpaceDataClient, SignalRSpaceDataClient>();

            // No Notification clients yet.
            container.Register<IStorageNotificationClient, StorageNotificationClientStub>();
            container.Register<IAccountNotificationClient, AccountNotificationClientStub>();
            container.Register<ISpaceNotificationClient, SpaceNotificationClientStub>();
        }
    }
}
