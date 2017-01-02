namespace EtAlii.Ubigia.Api.Transport.WebApi
{
    using System;
    using System.Net;
    using System.Threading.Tasks;

    public partial class WebApiAuthenticationDataClient : IAuthenticationDataClient
    {
        public async Task<Storage> GetConnectedStorage(ISpaceConnection connection)
        {
            if (connection.Storage != null)
            {
                throw new InvalidInfrastructureOperationException(InvalidInfrastructureOperation.SpaceAlreadyOpen);
            }

            var webApiConnection = (IWebApiConnection)connection;
            var localAddress = webApiConnection.AddressFactory.CreateFullAddress(connection.Configuration.Address, RelativeUri.Data.Storages) + "?local";
            var storage = await webApiConnection.Client.Get<Storage>(localAddress);

            if (storage == null)
            {
                throw new UnauthorizedInfrastructureOperationException(InvalidInfrastructureOperation.UnableToConnectToStorage);
            }

            // We do not want the address pushed to us from the server. 
            // If we get here then we already know how to contact the server. 
            storage.Address = connection.Configuration.Address;

            return storage;
        }
        public async Task<Storage> GetConnectedStorage(IStorageConnection connection)
        {
            if (connection.Storage != null)
            {
                throw new InvalidInfrastructureOperationException(InvalidInfrastructureOperation.StorageAlreadyOpen);
            }

            var webApiConnection = (IWebApiConnection)connection;
            var localAddress = webApiConnection.AddressFactory.CreateFullAddress(connection.Configuration.Address, RelativeUri.Data.Storages) + "?local";
            var storage = await webApiConnection.Client.Get<Storage>(localAddress);

            if (storage == null)
            {
                throw new UnauthorizedInfrastructureOperationException(InvalidInfrastructureOperation.UnableToConnectToStorage);
            }

            // We do not want the address pushed to us from the server. 
            // If we get here then we already know how to contact the server. 
            storage.Address = connection.Configuration.Address;

            return storage;
        }
    }
}
