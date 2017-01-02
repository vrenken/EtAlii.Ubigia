namespace EtAlii.Ubigia.Api.Transport.SignalR
{
    using System.Threading.Tasks;
    using Microsoft.AspNet.SignalR.Client;

    internal partial class SignalRContentDataClient : SignalRClientBase, IContentDataClient<ISignalRSpaceTransport>
    {
        private IHubProxy _contentProxy;
        private IHubProxy _contentDefinitionProxy;
        private readonly IHubProxyMethodInvoker _invoker;

        public SignalRContentDataClient(IHubProxyMethodInvoker invoker)
        {
            _invoker = invoker;
        }

        public override async Task Connect(ISpaceConnection<ISignalRSpaceTransport> spaceConnection)
        {
            await base.Connect(spaceConnection);

            await Task.Run(() =>
            {
                _contentProxy = spaceConnection.Transport.HubConnection.CreateHubProxy(SignalRHub.Content);
                _contentDefinitionProxy = spaceConnection.Transport.HubConnection.CreateHubProxy(SignalRHub.ContentDefinition);
            });
        }

        public override async Task Disconnect(ISpaceConnection<ISignalRSpaceTransport> spaceConnection)
        {
            await base.Disconnect(spaceConnection);

            await Task.Run(() =>
            {
                _contentDefinitionProxy = null;
                _contentProxy = null;
            });
        }
    }
}
