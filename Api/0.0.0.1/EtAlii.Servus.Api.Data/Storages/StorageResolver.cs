namespace EtAlii.Servus.Api
{
    using EtAlii.Servus.Client.Model;
    using System;

    public class StorageResolver
    {
        private readonly AddressFactory _addressFactory;
        private readonly Infrastructure _infrastructure;

        private const string _relativePath = "management/storage";

        public StorageResolver(Infrastructure infrastructure, AddressFactory addressFactory)
        {
            _infrastructure = infrastructure;
            _addressFactory = addressFactory;
        }

        public Storage Get(IStorageInfoProvider storageInfoProvider, Storage currentStorage, bool useCurrentStorage = true)
        {
            string address = null;

            //var currentStorage = StorageCmdlet.Current;

            if (storageInfoProvider.Storage != null)
            {
                address = _addressFactory.Create(currentStorage, _relativePath, UriParameter.StorageId, storageInfoProvider.Storage.Id.ToString());
            }
            else if (!String.IsNullOrWhiteSpace(storageInfoProvider.StorageName))
            {
                address = _addressFactory.Create(currentStorage, _relativePath, UriParameter.StorageName, storageInfoProvider.StorageName);
            }
            else if (storageInfoProvider.StorageId != Guid.Empty)
            {
                address = _addressFactory.Create(currentStorage, _relativePath, UriParameter.StorageId, storageInfoProvider.StorageId.ToString());
            }

            var storage = !String.IsNullOrWhiteSpace(address) ? _infrastructure.Get<Storage>(address) : null;

            if (storage == null && useCurrentStorage)
            {
                storage = storage ?? currentStorage;
            }
            return storage;
        }
    }
}
