// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Logical
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ILogicalStorageSet
    {
        IAsyncEnumerable<Storage> GetAll();

        Task<Storage> Get(string name);

        Task<Storage> Get(Guid id);

        Task<Storage> Add(Storage item);

        Task<Storage> AddLocalStorage(Storage item);

        Task Remove(Guid itemId);

        Task Remove(Storage itemToRemove);

        Task<Storage> Update(Guid itemId, Storage updatedItem);

        Task Start();
        Task Stop();
    }
}
