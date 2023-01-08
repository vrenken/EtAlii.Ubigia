// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Functional
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IStorageRepository
    {
        Task<Storage> GetLocal();
        IAsyncEnumerable<Storage> GetAll();
        Task<Storage> Get(string name);

        Task<Storage> Get(Guid itemId);

        Task<Storage> Add(Storage item);

        Task Remove(Guid itemId);
        Task Remove(Storage item);

        Task<Storage> Update(Guid itemId, Storage item);

        Task Initialize();
    }
}
