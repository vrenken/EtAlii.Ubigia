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

			_accountConnection = factory.Create(spaceConnection.Transport, spaceConnection.Storage.Address + SignalRHub.BasePath + "/" + SignalRHub.Account);
			_spaceConnection = factory.Create(spaceConnection.Transport, spaceConnection.Storage.Address + SignalRHub.BasePath + "/" + SignalRHub.Space);
	        await _accountConnection.StartAsync();
	        await _spaceConnection.StartAsync();
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
