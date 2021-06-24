// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Logical
{
    using System.Collections.Generic;

    public interface ILocalStorageGetter
    {
        Storage GetLocal(IList<Storage> items);
    }
}