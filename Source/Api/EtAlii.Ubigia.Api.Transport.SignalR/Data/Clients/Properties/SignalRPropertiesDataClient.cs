// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.SignalR;

using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;

internal sealed class SignalRPropertiesDataClient : SignalRClientBase, IPropertiesDataClient<ISignalRSpaceTransport>
{
    private HubConnection _connection;
    private readonly IHubProxyMethodInvoker _invoker;

    public SignalRPropertiesDataClient(
        IHubProxyMethodInvoker invoker)
    {
        _invoker = invoker;
    }


    public async Task Store(Identifier identifier, PropertyDictionary properties, ExecutionScope scope)
    {
        await _invoker.Invoke(_connection, SignalRHub.Property, "Post", identifier, properties).ConfigureAwait(false);

        PropertiesHelper.SetStored(properties, true);
    }

    public async Task<PropertyDictionary> Retrieve(Identifier identifier, ExecutionScope scope)
    {
        var result = await _invoker.Invoke<PropertyDictionary>(_connection, SignalRHub.Property, "Get", identifier).ConfigureAwait(false);
        if (result != null)
        {
            PropertiesHelper.SetStored(result, true);
            // properties.Stored is not serialized in the PropertyDictionaryConverter.
        }
        return result;
    }

    public override async Task Connect(ISpaceConnection<ISignalRSpaceTransport> spaceConnection)
    {
        await base.Connect(spaceConnection).ConfigureAwait(false);

        _connection = new HubConnectionFactory().Create(spaceConnection.Transport, new Uri(spaceConnection.Transport.Address + UriHelper.Delimiter + SignalRHub.Property, UriKind.Absolute));
        await _connection.StartAsync().ConfigureAwait(false);
    }

    public override async Task Disconnect()
    {
        await base.Disconnect().ConfigureAwait(false);

        await _connection.DisposeAsync().ConfigureAwait(false);
        _connection = null;
    }
}
