namespace EtAlii.Ubigia.Infrastructure.Logical
{
    using System.Collections.Generic;

    public interface ILocalStorageGetter
    {
        Storage GetLocal(IList<Storage> items);
    }
}