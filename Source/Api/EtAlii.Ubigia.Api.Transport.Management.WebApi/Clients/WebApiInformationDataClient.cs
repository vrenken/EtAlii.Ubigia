namespace EtAlii.Ubigia.Api.Transport.Management.WebApi
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport.WebApi;

    internal sealed class WebApiInformationDataClient : WebApiClientBase, IInformationDataClient
    {
        public async Task<Storage> GetConnectedStorage(IStorageConnection connection)
        {
            if (connection.Storage != null)
            {
                throw new InvalidInfrastructureOperationException(InvalidInfrastructureOperation.StorageAlreadyOpen);
            }

            var webApiConnection = (IWebApiConnection)connection;
            var localAddress = webApiConnection.AddressFactory.Create(connection.Transport.Address, RelativeDataUri.Storages, UriParameter.Local);
            var storage = await webApiConnection.Client.Get<Storage>(localAddress).ConfigureAwait(false);
			 
            if (storage == null)
            {
                throw new UnauthorizedInfrastructureOperationException(InvalidInfrastructureOperation.UnableToConnectToStorage);
            }

            return storage;
        }

        public async Task<ConnectivityDetails> GetConnectivityDetails(IStorageConnection connection)
        {
            var webApiConnection = (IWebApiConnection)connection;
            var address = webApiConnection.AddressFactory.Create(connection.Transport.Address, RelativeDataUri.Information, UriParameter.Connectivity);
            var connectivityDetails = await webApiConnection.Client.Get<ConnectivityDetails>(address).ConfigureAwait(false);
            return connectivityDetails;
        }
    }
}
