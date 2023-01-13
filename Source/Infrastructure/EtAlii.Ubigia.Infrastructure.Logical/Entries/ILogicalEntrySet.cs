// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Logical;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface ILogicalEntrySet
{
    /// <summary>
    /// Prepare an entry for storage of information.
    /// </summary>
    /// <param name="storageId"></param>
    /// <param name="spaceId"></param>
    /// <returns></returns>
    Task<Entry> Prepare(Guid storageId, Guid spaceId);

    /// <summary>
    /// Prepare an entry for storage of information.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<Entry> Prepare(Identifier id);

    /// <summary>
    /// Get the entry for the specified identifier. Only return an entry filled with the specified relations information.
    /// </summary>
    /// <param name="identifier"></param>
    /// <param name="entryRelations"></param>
    /// <returns></returns>
    Task<Entry> Get(Identifier identifier, EntryRelations entryRelations);

    /// <summary>
    /// Get the entries for the specified identifiers. Only return entries filled with the specified relations information.
    /// </summary>
    /// <param name="identifiers"></param>
    /// <param name="entryRelations"></param>
    /// <returns></returns>
    IAsyncEnumerable<Entry> Get(IEnumerable<Identifier> identifiers, EntryRelations entryRelations);

    /// <summary>
    /// Get related entries for the specified identifier. Only return entries filled with the specified relations information.
    /// </summary>
    /// <param name="identifier"></param>
    /// <param name="entriesWithRelation"></param>
    /// <param name="entryRelations"></param>
    /// <returns></returns>
    IAsyncEnumerable<Entry> GetRelated(Identifier identifier, EntryRelations entriesWithRelation, EntryRelations entryRelations);

    /// <summary>
    /// Store the entry in the backend.
    /// </summary>
    /// <param name="entry"></param>
    /// <returns></returns>
    Task<(Entry e, IEnumerable<IComponent> storedComponents)> Store(IEditableEntry entry);

    /// <summary>
    /// Store the entry in the backend.
    /// </summary>
    /// <param name="entry"></param>
    /// <returns></returns>
    Task<(Entry e, IEnumerable<IComponent> storedComponents)> Store(Entry entry);

    Task Update(Entry entry, IEnumerable<IComponent> changedComponents);
    Task Update(IEditableEntry entry, IEnumerable<IComponent> changedComponents);

}
