namespace EtAlii.Servus.Api.Management
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Servus.Api.Transport;
    using EtAlii.Servus.Api.Transport.WebApi;

    public class StorageResolver
    {
        private readonly IAddressFactory _addressFactory;
        private readonly IInfrastructureClient _client;

        public StorageResolver(IInfrastructureClient client, IAddressFactory addressFactory)
        {
            _client = client;
            _addressFactory = addressFactory;
        }

        public async Task<Storage> Get(IStorageInfoProvider storageInfoProvider, Storage currentStorage, bool useCurrentStorage = true)
        {
            string address = null;

            //var currentStorage = StorageCmdlet.Current;

            if (storageInfoProvider.Storage != null)
            {
                address = _addressFactory.Create(currentStorage, RelativeUri.Storages, UriParameter.StorageId, storageInfoProvider.Storage.Id.ToString());
            }
            else if (!String.IsNullOrWhiteSpace(storageInfoProvider.StorageName))
            {
                address = _addressFactory.Create(currentStorage, RelativeUri.Storages, UriParameter.StorageName, storageInfoProvider.StorageName);
            }
            else if (storageInfoProvider.StorageId != Guid.Empty)
            {
                address = _addressFactory.Create(currentStorage, RelativeUri.Storages, UriParameter.StorageId, storageInfoProvider.StorageId.ToString());
            }

            var storage = !String.IsNullOrWhiteSpace(address) ? await _client.Get<Storage>(address) : null;

            if (storage == null && useCurrentStorage)
            {
                storage = currentStorage;
            }
            return storage;
        }
    }
}
