namespace EtAlii.Ubigia.Api.Transport.WebApi
{
    using System.Threading.Tasks;

    public partial class WebApiAuthenticationDataClient
    {
        public async Task<Storage> GetConnectedStorage(ISpaceConnection connection)
        {
            if (connection.Storage != null)
            {
                throw new InvalidInfrastructureOperationException(InvalidInfrastructureOperation.SpaceAlreadyOpen);
            }

            var webApiConnection = (IWebApiConnection)connection;
            var localAddress = webApiConnection.AddressFactory.Create(connection.Transport.Address, RelativeUri.Data.Storages, UriParameter.Local);
			var storage = await webApiConnection.Client.Get<Storage>(localAddress);

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

            var webApiConnection = (IWebApiConnection)connection;
            var localAddress = webApiConnection.AddressFactory.Create(connection.Transport.Address, RelativeUri.Data.Storages, UriParameter.Local);
            var storage = await webApiConnection.Client.Get<Storage>(localAddress);
			 
            if (storage == null)
            {
                throw new UnauthorizedInfrastructureOperationException(InvalidInfrastructureOperation.UnableToConnectToStorage);
            }

            return storage;
        }
    }
}
