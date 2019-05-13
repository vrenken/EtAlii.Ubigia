namespace EtAlii.Ubigia.Infrastructure.Functional
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api;

    public interface IStorageRepository 
    {
        Storage GetLocal();
        Storage Get(string name);

        IEnumerable<Storage> GetAll();
        Storage Get(Guid itemId);

        Task<Storage> Add(Storage item);

        void Remove(Guid itemId);
        void Remove(Storage item);

        Storage Update(Guid itemId, Storage item);
    }
}