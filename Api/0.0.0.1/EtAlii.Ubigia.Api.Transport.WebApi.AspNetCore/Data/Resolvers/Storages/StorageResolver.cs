namespace EtAlii.Ubigia.Api.Transport.WebApi
{
    using System;
    using System.Threading.Tasks;

    public class StorageResolver : IStorageResolver
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
            Uri address = null;

            //var currentStorage = StorageCmdlet.Current

            if (storageInfoProvider.Storage != null)
            {
                address = _addressFactory.Create(currentStorage, RelativeUri.Data.Storages, UriParameter.StorageId, storageInfoProvider.Storage.Id.ToString());
            }
            else if (!String.IsNullOrWhiteSpace(storageInfoProvider.StorageName))
            {
                address = _addressFactory.Create(currentStorage, RelativeUri.Data.Storages, UriParameter.StorageName, storageInfoProvider.StorageName);
            }
            else if (storageInfoProvider.StorageId != Guid.Empty)
            {
                address = _addressFactory.Create(currentStorage, RelativeUri.Data.Storages, UriParameter.StorageId, storageInfoProvider.StorageId.ToString());
            }

            var storage = address != null ? await _client.Get<Storage>(address) : null;

            if (storage == null && useCurrentStorage)
            {
                storage = currentStorage;
            }
            return storage;
        }
    }
}
