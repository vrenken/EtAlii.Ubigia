namespace EtAlii.Servus.Infrastructure.Logical
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using EtAlii.Servus.Api;

    public interface ILogicalStorageSet
    {
        Storage GetLocal();

        Storage Get(string name);

        Storage Add(Storage item);

        IEnumerable<Storage> GetAll();

        Storage Get(Guid id);

        ObservableCollection<Storage> GetItems();

        void Remove(Guid itemId);

        void Remove(Storage itemToRemove);

        Storage Update(Guid itemId, Storage updatedItem);

        void Start();
        void Stop();

        event EventHandler<Storage> LocalStorageInitialized;
        event EventHandler<Storage> StorageInitialized;
    }
}