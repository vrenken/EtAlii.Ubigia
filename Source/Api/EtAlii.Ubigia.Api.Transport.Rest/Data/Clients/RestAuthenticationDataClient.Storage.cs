// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Rest
{
    using System.Threading.Tasks;

    public partial class RestAuthenticationDataClient
    {
        public async Task<Storage> GetConnectedStorage(ISpaceConnection connection)
        {
            if (connection.Storage != null)
            {
                throw new InvalidInfrastructureOperationException(InvalidInfrastructureOperation.SpaceAlreadyOpen);
            }

            var restConnection = (IRestConnection)connection;
            var localAddress = restConnection.AddressFactory.Create(connection.Transport.Address, RelativeDataUri.Storages, UriParameter.Local);
			var storage = await restConnection.Client.Get<Storage>(localAddress).ConfigureAwait(false);

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
                throw new InvalidInfrastructureOperationException(InvalidInfrastructureOperation.StorageAlreadyOpen);
            }

            var restConnection = (IRestConnection)connection;
            var localAddress = restConnection.AddressFactory.Create(connection.Transport.Address, RelativeDataUri.Storages, UriParameter.Local);
            var storage = await restConnection.Client.Get<Storage>(localAddress).ConfigureAwait(false);

            if (storage == null)
            {
                throw new UnauthorizedInfrastructureOperationException(InvalidInfrastructureOperation.UnableToConnectToStorage);
            }

            return storage;
        }
    }
}
