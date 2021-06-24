// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Logical
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ILogicalStorageSet
    {
        Storage GetLocal();

        IAsyncEnumerable<Storage> GetAll();

        Storage Get(string name);
        
        Storage Get(Guid id);

        Task<Storage> Add(Storage item);

        void Remove(Guid itemId);

        void Remove(Storage itemToRemove);

        Storage Update(Guid itemId, Storage updatedItem);

        Task Start();
        Task Stop();

        Func<Storage, Task> Initialized { get; set; }
        Func<Storage, Task> Added { get; set; }
    }
}