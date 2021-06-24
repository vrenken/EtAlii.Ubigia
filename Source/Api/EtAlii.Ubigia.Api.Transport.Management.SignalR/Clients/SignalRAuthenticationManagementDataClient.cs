// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Management.SignalR
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport.SignalR;
    using Microsoft.AspNetCore.SignalR.Client;

    public partial class SignalRAuthenticationManagementDataClient : SignalRManagementClientBase, IAuthenticationManagementDataClient<ISignalRStorageTransport>
    {
        private HubConnection _accountConnection;
        private HubConnection _storageConnection;
        private readonly ISignalRAuthenticationTokenGetter _signalRAuthenticationTokenGetter;

        public SignalRAuthenticationManagementDataClient(ISignalRAuthenticationTokenGetter signalRAuthenticationTokenGetter)
        {
            _signalRAuthenticationTokenGetter = signalRAuthenticationTokenGetter;
        }

        public override async Task Connect(IStorageConnection<ISignalRStorageTransport> storageConnection)
        {
            await base.Connect(storageConnection).ConfigureAwait(false);

            var factory = new HubConnectionFactory();

			_accountConnection = factory.Create(storageConnection.Transport, new Uri(storageConnection.Transport.Address + UriHelper.Delimiter + SignalRHub.Account, UriKind.Absolute));
			_storageConnection = factory.Create(storageConnection.Transport, new Uri(storageConnection.Transport.Address + UriHelper.Delimiter + SignalRHub.Storage, UriKind.Absolute));
	        await _accountConnection.StartAsync().ConfigureAwait(false);
	        await _storageConnection.StartAsync().ConfigureAwait(false);
        }

        public override async Task Disconnect(IStorageConnection<ISignalRStorageTransport> storageConnection)
        {
            await base.Disconnect(storageConnection).ConfigureAwait(false);

            await _accountConnection.DisposeAsync().ConfigureAwait(false);
            _accountConnection = null;
            await _storageConnection.DisposeAsync().ConfigureAwait(false);
            _storageConnection = null;
        }
    }
}
