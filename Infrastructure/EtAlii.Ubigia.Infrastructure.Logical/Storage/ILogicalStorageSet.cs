namespace EtAlii.Ubigia.Infrastructure.Logical
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api;

    public interface ILogicalStorageSet
    {
        Storage GetLocal();

        Storage Get(string name);

        Task<Storage> Add(Storage item);

        IEnumerable<Storage> GetAll();

        Storage Get(Guid id);

        ObservableCollection<Storage> GetItems();

        void Remove(Guid itemId);

        void Remove(Storage itemToRemove);

        Storage Update(Guid itemId, Storage updatedItem);

        Task Start();
        Task Stop();

        Func<Storage, Task> Initialized { get; set; }
        Func<Storage, Task> Added { get; set; }
    }
}