namespace EtAlii.Ubigia.Api.Transport.SignalR
{
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
            await base.Connect(spaceConnection);
            
            var factory = new HubConnectionFactory();

	        _contentConnection = factory.Create(spaceConnection.Transport, new Uri(spaceConnection.Storage.Address + "/" + SignalRHub.Content, UriKind.Absolute));
            _contentDefinitionConnection = factory.Create(spaceConnection.Transport, new Uri(spaceConnection.Storage.Address + "/" + SignalRHub.ContentDefinition, UriKind.Absolute));

	        await _contentConnection.StartAsync();
	        await _contentDefinitionConnection.StartAsync();
        }

        public override async Task Disconnect()
        {
            await base.Disconnect();

            await _contentDefinitionConnection.DisposeAsync();
            _contentDefinitionConnection = null;
            await _contentConnection.DisposeAsync();
            _contentConnection = null;
        }
    }
}
