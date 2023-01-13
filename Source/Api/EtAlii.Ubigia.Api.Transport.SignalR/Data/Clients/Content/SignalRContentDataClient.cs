// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.SignalR;

using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;

internal partial class SignalRContentDataClient : SignalRClientBase, IContentDataClient<ISignalRSpaceTransport>
{
    private HubConnection _contentConnection;
    private HubConnection _contentDefinitionConnection;
    private readonly IHubProxyMethodInvoker _invoker;

    public SignalRContentDataClient(IHubProxyMethodInvoker invoker)
    {
        _invoker = invoker;
    }

    public override async Task Connect(ISpaceConnection<ISignalRSpaceTransport> spaceConnection)
    {
        await base.Connect(spaceConnection).ConfigureAwait(false);

        var factory = new HubConnectionFactory();

        _contentConnection = factory.Create(spaceConnection.Transport, new Uri(spaceConnection.Transport.Address + UriHelper.Delimiter + SignalRHub.Content, UriKind.Absolute));
        _contentDefinitionConnection = factory.Create(spaceConnection.Transport, new Uri(spaceConnection.Transport.Address + UriHelper.Delimiter + SignalRHub.ContentDefinition, UriKind.Absolute));

        await _contentConnection.StartAsync().ConfigureAwait(false);
        await _contentDefinitionConnection.StartAsync().ConfigureAwait(false);
    }

    public override async Task Disconnect()
    {
        await base.Disconnect().ConfigureAwait(false);

        await _contentDefinitionConnection.DisposeAsync().ConfigureAwait(false);
        _contentDefinitionConnection = null;
        await _contentConnection.DisposeAsync().ConfigureAwait(false);
        _contentConnection = null;
    }
}
