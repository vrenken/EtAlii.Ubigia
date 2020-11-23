namespace EtAlii.Ubigia.Api.Transport.Management.SignalR
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport.SignalR;
    using Microsoft.AspNetCore.SignalR.Client;

    public sealed class SignalRInformationDataClient : IInformationDataClient<ISignalRStorageTransport>
    {
        private HubConnection _connection;
        private readonly IHubProxyMethodInvoker _invoker;

        public SignalRInformationDataClient(IHubProxyMethodInvoker invoker)
        {
            _invoker = invoker;
        }
        
        public async Task Connect(IStorageConnection storageConnection)
        {
            await Connect((IStorageConnection<ISignalRStorageTransport>) storageConnection).ConfigureAwait(false);
        }

        public async Task Disconnect(IStorageConnection storageConnection)
        {
            await Disconnect((IStorageConnection<ISignalRStorageTransport>) storageConnection).ConfigureAwait(false);
        }

        public async Task Connect(IStorageConnection<ISignalRStorageTransport> storageConnection)
        {
            _connection = new HubConnectionFactory().Create(storageConnection.Transport, new Uri(storageConnection.Transport.Address + "/" + SignalRHub.Space, UriKind.Absolute));
            await _connection.StartAsync().ConfigureAwait(false);
        }

        public async Task Disconnect(IStorageConnection<ISignalRStorageTransport> storageConnection)
        {
            await _connection.DisposeAsync().ConfigureAwait(false);
            _connection = null;
        }

        public async Task<Storage> GetConnectedStorage(IStorageConnection storageConnection)
        {
            if (storageConnection.Storage != null)
            {
                throw new InvalidInfrastructureOperationException(InvalidInfrastructureOperation.StorageAlreadyOpen);
            }

            var signalRConnection = (ISignalRStorageConnection)storageConnection;

            var storage = await GetConnectedStorage(signalRConnection.Transport).ConfigureAwait(false);

            if (storage == null)
            {
                throw new UnauthorizedInfrastructureOperationException(InvalidInfrastructureOperation.UnableToConnectToStorage);
            }

            //// We do not want the address pushed to us from the server. 
            //// If we get here then we already know how to contact the server. 
            //storage.Address = storageConnection.Transport.Address.ToString[]

            return storage;
        }

        private async Task<Storage> GetConnectedStorage(ISignalRStorageTransport transport)
        {
            var connection = new HubConnectionFactory().Create(transport,new Uri(transport.Address + "/" + SignalRHub.Information), transport.AuthenticationToken);
            await connection.StartAsync().ConfigureAwait(false);
            var storage = await _invoker.Invoke<Storage>(connection, SignalRHub.Information, "GetLocalStorage").ConfigureAwait(false);
            await connection.DisposeAsync().ConfigureAwait(false);
            return storage;
        }

        public async Task<ConnectivityDetails> GetConnectivityDetails(IStorageConnection storageConnection)
        {
            var signalRConnection = (ISignalRStorageConnection)storageConnection;
            var transport = signalRConnection.Transport;

            var connection = new HubConnectionFactory().Create(transport,new Uri(transport.Address + "/" + SignalRHub.Information));
            await connection.StartAsync().ConfigureAwait(false);

            var details = await _invoker.Invoke<ConnectivityDetails>(connection, SignalRHub.Information, "GetLocalConnectivityDetails").ConfigureAwait(false);
            await connection.DisposeAsync().ConfigureAwait(false);
            return details;
        }
    }
}
