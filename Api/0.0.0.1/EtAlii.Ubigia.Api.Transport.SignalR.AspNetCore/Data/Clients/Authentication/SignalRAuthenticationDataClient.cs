namespace EtAlii.Ubigia.Api.Transport.SignalR
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.SignalR.Client;

    public partial class SignalRAuthenticationDataClient : SignalRClientBase, IAuthenticationDataClient<ISignalRSpaceTransport>
    {
        private HubConnection _accountConnection;
        private HubConnection _spaceConnection;
        private readonly IHubProxyMethodInvoker _invoker;

        public SignalRAuthenticationDataClient(
            IHubProxyMethodInvoker invoker)
        {
            _invoker = invoker;
            _hostIdentifier = CreateHostIdentifier();

        }

        public override async Task Connect(ISpaceConnection<ISignalRSpaceTransport> spaceConnection)
        {
            await base.Connect(spaceConnection);

            var factory = new HubConnectionFactory();
            await Task.Run(() =>
            {
                _accountConnection = factory.Create(spaceConnection.Storage.Address + "/" + SignalRHub.Account, spaceConnection.Transport.HttpClientHandler);
                _spaceConnection = factory.Create(spaceConnection.Storage.Address + "/" + SignalRHub.Space, spaceConnection.Transport.HttpClientHandler);
                //_accountProxy = spaceConnection.Transport.HubConnection.CreateHubProxy(SignalRHub.Account);
                //_spaceProxy = spaceConnection.Transport.HubConnection.CreateHubProxy(SignalRHub.Space);

                // Also let's set the correct authentication token. 
                spaceConnection.Transport.HttpClientHandler.AuthenticationToken = spaceConnection.Transport.AuthenticationToken;
            });
        }

        public override async Task Disconnect(ISpaceConnection<ISignalRSpaceTransport> spaceConnection)
        {
            await base.Disconnect(spaceConnection);

            await _accountConnection.DisposeAsync();
            _accountConnection = null;
            await _spaceConnection.DisposeAsync();
            _spaceConnection = null;
        }
    }
}
