namespace EtAlii.Ubigia.Infrastructure.Logical
{
    using System.Collections.Generic;
    using EtAlii.Ubigia.Api;

    public interface ILocalStorageGetter
    {
        Storage GetLocal(IList<Storage> items);
    }
}