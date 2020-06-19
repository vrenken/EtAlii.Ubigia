namespace EtAlii.Ubigia.Api.Transport.Management.WebApi
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport.WebApi;

    internal sealed class WebApiInformationDataClient : WebApiClientBase, IInformationDataClient
    {
        public async Task<Storage> GetConnectedStorage(IStorageConnection storageConnection)
        {
            if (storageConnection.Storage != null)
            {
                throw new InvalidInfrastructureOperationException(InvalidInfrastructureOperation.StorageAlreadyOpen);
            }

            var webApiConnection = (IWebApiConnection)storageConnection;
            var localAddress = webApiConnection.AddressFactory.Create(storageConnection.Transport.Address, RelativeUri.Data.Storages, UriParameter.Local);
            var storage = await webApiConnection.Client.Get<Storage>(localAddress);
			 
            if (storage == null)
            {
                throw new UnauthorizedInfrastructureOperationException(InvalidInfrastructureOperation.UnableToConnectToStorage);
            }

            // We do not want the address pushed to us from the server. 
            // If we get here then we already know how to contact the server. 
            storage.Address = storageConnection.Transport.Address.ToString();

            return storage;
        }

        public async Task<ConnectivityDetails> GetConnectivityDetails(IStorageConnection storageConnection)
        {
            var webApiConnection = (IWebApiConnection)storageConnection;
            var address = webApiConnection.AddressFactory.Create(storageConnection.Storage, RelativeUri.Data.Information, UriParameter.Connectivity);
            var connectivityDetails = await webApiConnection.Client.Get<ConnectivityDetails>(address);
            return connectivityDetails;
        }
    }
}
