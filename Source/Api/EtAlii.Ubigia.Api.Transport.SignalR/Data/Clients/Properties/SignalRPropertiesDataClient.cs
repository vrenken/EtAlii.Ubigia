﻿namespace EtAlii.Ubigia.Api.Transport.SignalR
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.SignalR.Client;

    internal class SignalRPropertiesDataClient : SignalRClientBase, IPropertiesDataClient<ISignalRSpaceTransport>
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
            return await scope.Cache.GetProperties(identifier, async () =>
            {
                var result = await _invoker.Invoke<PropertyDictionary>(_connection, SignalRHub.Property, "Get", identifier).ConfigureAwait(false);
                if (result != null)
                {
                    PropertiesHelper.SetStored(result, true);
                    // properties.Stored is not serialized in the PropertyDictionaryConverter.
                }
                return result;
            }).ConfigureAwait(false);
        }

        public override async Task Connect(ISpaceConnection<ISignalRSpaceTransport> spaceConnection)
        {
            await base.Connect(spaceConnection).ConfigureAwait(false);

            _connection = new HubConnectionFactory().Create(spaceConnection.Transport, new Uri(spaceConnection.Transport.Address + "/" + SignalRHub.Property, UriKind.Absolute));
	        await _connection.StartAsync().ConfigureAwait(false);
        }

        public override async Task Disconnect() 
        {
            await base.Disconnect().ConfigureAwait(false);

            await _connection.DisposeAsync().ConfigureAwait(false);
            _connection = null;
        }
    }
}
