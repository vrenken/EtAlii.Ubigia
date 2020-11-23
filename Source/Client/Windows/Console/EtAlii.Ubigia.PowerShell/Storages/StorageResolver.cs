namespace EtAlii.Ubigia.PowerShell.Storages
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport.WebApi;

    public class StorageResolver : IStorageResolver
    {
        private readonly IAddressFactory _addressFactory;
        private readonly IInfrastructureClient _client;

        public StorageResolver(IInfrastructureClient client, IAddressFactory addressFactory)
        {
            _client = client;
            _addressFactory = addressFactory;
        }

        public async Task<Storage> Get(IStorageInfoProvider storageInfoProvider, Storage currentStorage, Uri currentStorageApiAddress, bool useCurrentStorage = true)
        {
            Uri address = null;

            //var currentStorage = StorageCmdlet.Current

            if (storageInfoProvider.Storage != null)
            {
                address = _addressFactory.Create(currentStorageApiAddress, RelativeUri.Data.Storages, UriParameter.StorageId, storageInfoProvider.Storage.Id.ToString());
            }
            else if (!string.IsNullOrWhiteSpace(storageInfoProvider.StorageName))
            {
                address = _addressFactory.Create(currentStorageApiAddress, RelativeUri.Data.Storages, UriParameter.StorageName, storageInfoProvider.StorageName);
            }
            else if (storageInfoProvider.StorageId != Guid.Empty)
            {
                address = _addressFactory.Create(currentStorageApiAddress, RelativeUri.Data.Storages, UriParameter.StorageId, storageInfoProvider.StorageId.ToString());
            }

            var storage = address != null ? await _client.Get<Storage>(address).ConfigureAwait(false) : null;

            if (storage == null && useCurrentStorage)
            {
                storage = currentStorage;
            }
            return storage;
        }
    }
}
