namespace EtAlii.Ubigia.Api.Transport.Management.SignalR
{
	using System;
    using System.Security.Cryptography;
    using System.Threading.Tasks;
	using EtAlii.Ubigia.Api.Transport.SignalR;
	using Microsoft.AspNetCore.SignalR.Client;

    public partial class SignalRAuthenticationManagementDataClient : SignalRManagementClientBase, IAuthenticationManagementDataClient<ISignalRStorageTransport>
    {
        private HubConnection _accountConnection;
        private HubConnection _storageConnection;
        private readonly IHubProxyMethodInvoker _invoker;

        public SignalRAuthenticationManagementDataClient(
            IHubProxyMethodInvoker invoker)
        {
            _invoker = invoker;
            _hostIdentifier = CreateHostIdentifier();
            _random = RandomNumberGenerator.Create();
        }

        public override async Task Connect(IStorageConnection<ISignalRStorageTransport> storageConnection)
        {
            await base.Connect(storageConnection);

            var factory = new HubConnectionFactory();

			_accountConnection = factory.Create(storageConnection.Transport, new Uri(storageConnection.Storage.Address + SignalRHub.BasePath + UriConstant.PathSeparator + SignalRHub.Account, UriKind.Absolute));
			_storageConnection = factory.Create(storageConnection.Transport, new Uri(storageConnection.Storage.Address + SignalRHub.BasePath + UriConstant.PathSeparator + SignalRHub.Storage, UriKind.Absolute));
	        await _accountConnection.StartAsync();
	        await _storageConnection.StartAsync();
        }

        public override async Task Disconnect(IStorageConnection<ISignalRStorageTransport> storageConnection)
        {
            await base.Disconnect(storageConnection);

            await _accountConnection.DisposeAsync();
            _accountConnection = null;
            await _storageConnection.DisposeAsync();
            _storageConnection = null;
        }
    }
}
