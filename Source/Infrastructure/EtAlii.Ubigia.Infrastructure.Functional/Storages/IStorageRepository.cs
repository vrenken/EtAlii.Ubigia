// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Functional
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IStorageRepository 
    {
        Storage GetLocal();
        IAsyncEnumerable<Storage> GetAll();
        Storage Get(string name);

        Storage Get(Guid itemId);

        Task<Storage> Add(Storage item);

        void Remove(Guid itemId);
        void Remove(Storage item);

        Storage Update(Guid itemId, Storage item);
    }
}