namespace EtAlii.Servus.Infrastructure.Functional
{
    using System;
    using System.Collections.Generic;
    using EtAlii.Servus.Api;

    public interface IStorageRepository 
    {
        Storage GetLocal();
        Storage Get(string name);

        IEnumerable<Storage> GetAll();
        Storage Get(Guid itemId);

        Storage Add(Storage item);

        void Remove(Guid itemId);
        void Remove(Storage item);

        Storage Update(Guid itemId, Storage item);
    }
}