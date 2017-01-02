namespace EtAlii.Servus.PowerShell.Storages
{
    using EtAlii.Servus.Client.Model;
    using System;

    public class StorageResolver
    {
        private readonly AddressFactory _addressFactory;
        private readonly Infrastructure _infrastructure;

        public StorageResolver(Infrastructure infrastructure, AddressFactory addressFactory)
        {
            _infrastructure = infrastructure;
            _addressFactory = addressFactory;
        }

        public Storage Get(IStorageInfoProvider storageInfoProvider, bool useCurrentStorage = true)
        {
            string address = null;

            var targetStorage = StorageCmdlet.Current;

            if (storageInfoProvider.Storage != null)
            {
                address = _addressFactory.Create(targetStorage, "management/storage", "id", storageInfoProvider.Storage.Id.ToString());
            }
            else if (!String.IsNullOrWhiteSpace(storageInfoProvider.StorageName))
            {
                address = _addressFactory.Create(targetStorage, "management/storage", "name", storageInfoProvider.StorageName);
            }
            else if (storageInfoProvider.StorageId != Guid.Empty)
            {
                address = _addressFactory.Create(targetStorage, "management/storage", "id", storageInfoProvider.StorageId.ToString());
            }

            var storage = !String.IsNullOrWhiteSpace(address) ? _infrastructure.Get<Storage>(address) : null;

            if (storage == null && useCurrentStorage)
            {
                storage = storage ?? StorageCmdlet.Current;
            }
            return storage;
        }
    }
}
