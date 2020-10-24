﻿namespace EtAlii.Ubigia.Api.Transport.SignalR
{
    using System;
    using System.Threading.Tasks;

    public partial class SignalRAuthenticationDataClient
    {
        public async Task<Storage> GetConnectedStorage(ISpaceConnection connection)
        {
            if (connection.Storage != null)
            {
                throw new InvalidInfrastructureOperationException(InvalidInfrastructureOperation.SpaceAlreadyOpen);
            }

            var signalRConnection = (ISignalRSpaceConnection) connection;
            var storage = await GetConnectedStorage(
	            signalRConnection.Transport,
				connection.Transport.Address);

            if (storage == null)
            {
                throw new UnauthorizedInfrastructureOperationException(InvalidInfrastructureOperation.UnableToConnectToStorage);
            }

            return storage;
        }

        public async Task<Storage> GetConnectedStorage(IStorageConnection connection)
        {
            if (connection.Storage != null)
            {
                throw new InvalidInfrastructureOperationException(InvalidInfrastructureOperation.SpaceAlreadyOpen);
            }

            var signalRConnection = (ISignalRStorageConnection)connection;

            var storage = await GetConnectedStorage(
	            signalRConnection.Transport,
				connection.Transport.Address);

            if (storage == null)
            {
                throw new UnauthorizedInfrastructureOperationException(InvalidInfrastructureOperation.UnableToConnectToStorage);
            }

            return storage;
        }

        private async Task<Storage> GetConnectedStorage(
	        ISignalRTransport transport,
	        Uri address)
        {
			var connection = new HubConnectionFactory().Create(transport, new Uri(address + "/" + SignalRHub.Authentication), transport.AuthenticationToken);
            await connection.StartAsync();
            var storage = await _invoker.Invoke<Storage>(connection, SignalRHub.Authentication, "GetLocalStorage");
            await connection.DisposeAsync();
            return storage;
        }
    }
}