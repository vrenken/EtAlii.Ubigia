// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.SignalR;

using EtAlii.xTechnology.MicroContainer;

internal sealed class SignalRSpaceClientsScaffolding : IScaffolding
{
    private readonly SpaceConnectionOptions _options;

    public SignalRSpaceClientsScaffolding(SpaceConnectionOptions options)
    {
        _options = options;
    }

    public void Register(IRegisterOnlyContainer container)
    {
        container.Register<ISpaceConnection>(serviceCollection =>
        {
            var transport = serviceCollection.GetInstance<ISpaceTransport>();
            var roots = serviceCollection.GetInstance<IRootContext>();
            var entries = serviceCollection.GetInstance<IEntryContext>();
            var content = serviceCollection.GetInstance<IContentContext>();
            var properties = serviceCollection.GetInstance<IPropertiesContext>();
            var authentication = serviceCollection.GetInstance<IAuthenticationContext>();
            return new SignalRSpaceConnection(transport, _options, roots, entries, content, properties, authentication);
        });

        container.Register<IHubProxyMethodInvoker, HubProxyMethodInvoker>();

        container.Register<IAuthenticationDataClient, SignalRAuthenticationDataClient>();
        container.Register<ISignalRAuthenticationTokenGetter, SignalRAuthenticationTokenGetter>();

        container.Register<IEntryDataClient, SignalREntryDataClient>();
        container.Register<IRootDataClient, SignalRRootDataClient>();
        container.Register<IPropertiesDataClient, SignalRPropertiesDataClient>();
        container.Register<IContentDataClient, SignalRContentDataClient>();
    }
}
