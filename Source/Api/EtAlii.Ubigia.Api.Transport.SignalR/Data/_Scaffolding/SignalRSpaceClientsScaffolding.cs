// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.SignalR
{
    using EtAlii.xTechnology.MicroContainer;

    internal class SignalRSpaceClientsScaffolding : IScaffolding
    {
        public void Register(IRegisterOnlyContainer container)
        {
            container.Register<ISpaceConnection, SignalRSpaceConnection>();

            container.Register<IHubProxyMethodInvoker, HubProxyMethodInvoker>();

            container.Register<IAuthenticationDataClient, SignalRAuthenticationDataClient>();
            container.Register<ISignalRAuthenticationTokenGetter, SignalRAuthenticationTokenGetter>();

            container.Register<IEntryDataClient, SignalREntryDataClient>();
            container.Register<IEntryNotificationClient, SignalREntryNotificationClient>();

            container.Register<IRootDataClient, SignalRRootDataClient>();
            container.Register<IRootNotificationClient, SignalRRootNotificationClient>();

            container.Register<IPropertiesDataClient, SignalRPropertiesDataClient>();
            container.Register<IPropertiesNotificationClient, SignalRPropertiesNotificationClient>();

            container.Register<IContentDataClient, SignalRContentDataClient>();
            container.Register<IContentNotificationClient, SignalRContentNotificationClient>();
        }
    }
}
