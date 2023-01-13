// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Fabric;

using System.Collections.Generic;
using System.Threading.Tasks;

public interface IEntryStorer
{
    /// <summary>
    /// Store the specified entry and return the new entry and stored components.
    /// </summary>
    /// <param name="entry"></param>
    /// <returns></returns>
    Task<(Entry e, IEnumerable<IComponent> storedComponents)> Store(IEditableEntry entry);

    /// <summary>
    /// Store the specified entry and return the new entry and stored components.
    /// </summary>
    /// <param name="entry"></param>
    /// <returns></returns>
    Task<(Entry e, IEnumerable<IComponent> storedComponents)> Store(Entry entry);
}
