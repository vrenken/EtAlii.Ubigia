namespace EtAlii.Ubigia.Api.Transport.SignalR
{
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
            await Task.Run(() =>
            {
                _contentConnection = factory.Create(spaceConnection.Storage.Address + "/" + SignalRHub.Content, spaceConnection.Transport.HttpClientHandler);
                _contentDefinitionConnection = factory.Create(spaceConnection.Storage.Address + "/" + SignalRHub.ContentDefinition, spaceConnection.Transport.HttpClientHandler);
                //_contentProxy = spaceConnection.Transport.HubConnection.CreateHubProxy(SignalRHub.Content);
                //_contentDefinitionProxy = spaceConnection.Transport.HubConnection.CreateHubProxy(SignalRHub.ContentDefinition);
            });
        }

        public override async Task Disconnect(ISpaceConnection<ISignalRSpaceTransport> spaceConnection)
        {
            await base.Disconnect(spaceConnection);

            await _contentDefinitionConnection.DisposeAsync();
            _contentDefinitionConnection = null;
            await _contentConnection.DisposeAsync();
            _contentConnection = null;
        }
    }
}
