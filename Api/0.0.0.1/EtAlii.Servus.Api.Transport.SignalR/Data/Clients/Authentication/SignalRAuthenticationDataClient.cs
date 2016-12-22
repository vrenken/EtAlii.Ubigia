namespace EtAlii.Servus.Api.Transport.SignalR
{
    using System.Threading.Tasks;
    using Microsoft.AspNet.SignalR.Client;

    public partial class SignalRAuthenticationDataClient : SignalRClientBase, IAuthenticationDataClient<ISignalRSpaceTransport>
    {
        private IHubProxy _accountProxy;
        private IHubProxy _spaceProxy;
        private readonly IHubProxyMethodInvoker _invoker;

        public SignalRAuthenticationDataClient(
            IHubProxyMethodInvoker invoker)
        {
            _invoker = invoker;
            _hostIdentifier = this.CreateHostIdentifier();

        }

        public override async Task Connect(ISpaceConnection<ISignalRSpaceTransport> spaceConnection)
        {
            await base.Connect(spaceConnection);

            await Task.Run(() =>
            {
                _accountProxy = spaceConnection.Transport.HubConnection.CreateHubProxy(SignalRHub.Account);
                _spaceProxy = spaceConnection.Transport.HubConnection.CreateHubProxy(SignalRHub.Space);

                // Also let's set the correct authentication token. 
                spaceConnection.Transport.HubConnection.Headers["Authentication-Token"] = spaceConnection.Transport.AuthenticationToken;
            });
        }

        public override async Task Disconnect(ISpaceConnection<ISignalRSpaceTransport> spaceConnection)
        {
            await base.Disconnect(spaceConnection);

            await Task.Run(() =>
            {
                _accountProxy = null;
                _spaceProxy = null;
            });
        }
    }
}
