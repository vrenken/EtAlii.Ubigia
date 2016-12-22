namespace EtAlii.Servus.Infrastructure.Logical
{
    using System.Collections.Generic;
    using EtAlii.Servus.Api;

    public interface ILocalStorageGetter
    {
        Storage GetLocal(IList<Storage> items);
    }
}