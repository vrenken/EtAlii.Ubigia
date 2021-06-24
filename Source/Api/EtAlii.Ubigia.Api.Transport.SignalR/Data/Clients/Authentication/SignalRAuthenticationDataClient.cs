// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

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
            await base.Connect(spaceConnection).ConfigureAwait(false);

            var factory = new HubConnectionFactory();

			_accountConnection = factory.Create(spaceConnection.Transport, new Uri(spaceConnection.Transport.Address + UriHelper.Delimiter + SignalRHub.Account, UriKind.Absolute));
			_spaceConnection = factory.Create(spaceConnection.Transport, new Uri(spaceConnection.Transport.Address + UriHelper.Delimiter + SignalRHub.Space, UriKind.Absolute));
	        await _accountConnection.StartAsync().ConfigureAwait(false);
	        await _spaceConnection.StartAsync().ConfigureAwait(false);
        }

        public override async Task Disconnect()
        {
            await base.Disconnect().ConfigureAwait(false);

            await _accountConnection.DisposeAsync().ConfigureAwait(false);
            _accountConnection = null;
            await _spaceConnection.DisposeAsync().ConfigureAwait(false);
            _spaceConnection = null;
        }
    }
}
