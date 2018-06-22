namespace EtAlii.Ubigia.Api.Transport.Management.WebApi
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport.WebApi;

    public partial class WebApiAuthenticationManagementDataClient
    {
        public async Task<Storage> GetConnectedStorage(IStorageConnection connection)
        {
            if (connection.Storage != null)
            {
                throw new InvalidInfrastructureOperationException(InvalidInfrastructureOperation.StorageAlreadyOpen);
            }

            var webApiConnection = (IWebApiConnection)connection;
            var localAddress = webApiConnection.AddressFactory.Create(connection.Configuration.Address, RelativeUri.Data.Storages, UriParameter.Local);
            var storage = await webApiConnection.Client.Get<Storage>(localAddress);
			 
            if (storage == null)
            {
                throw new UnauthorizedInfrastructureOperationException(InvalidInfrastructureOperation.UnableToConnectToStorage);
            }

            // We do not want the address pushed to us from the server. 
            // If we get here then we already know how to contact the server. 
            storage.Address = connection.Configuration.Address.ToString();

            return storage;
        }
    }
}
