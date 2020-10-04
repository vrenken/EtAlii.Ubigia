namespace EtAlii.Ubigia.Api.Transport.SignalR
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.SignalR.Client;

    public partial class SignalRAuthenticationDataClient : SignalRClientBase, IAuthenticationDataClient<ISignalRSpaceTransport>
    {
        private HubConnection _accountConnection;
        private HubConnection _spaceConnection;
        private readonly IHubProxyMethodInvoker _invoker;
        private readonly ISignalRAuthenticationTokenGetter _signalRAuthenticationTokenGetter;

        public SignalRAuthenticationDataClient(IHubProxyMethodInvoker invoker, ISignalRAuthenticationTokenGetter signalRAuthenticationTokenGetter)
        {
            _invoker = invoker;
            _signalRAuthenticationTokenGetter = signalRAuthenticationTokenGetter;
        }

        public override async Task Connect(ISpaceConnection<ISignalRSpaceTransport> spaceConnection)
        {
            await base.Connect(spaceConnection);

            var factory = new HubConnectionFactory();

			_accountConnection = factory.Create(spaceConnection.Transport, new Uri(spaceConnection.Transport.Address + "/" + SignalRHub.Account, UriKind.Absolute));
			_spaceConnection = factory.Create(spaceConnection.Transport, new Uri(spaceConnection.Transport.Address + "/" + SignalRHub.Space, UriKind.Absolute));
	        await _accountConnection.StartAsync();
	        await _spaceConnection.StartAsync();
        }

        public override async Task Disconnect()
        {
            await base.Disconnect();

            await _accountConnection.DisposeAsync();
            _accountConnection = null;
            await _spaceConnection.DisposeAsync();
            _spaceConnection = null;
        }
    }
}
