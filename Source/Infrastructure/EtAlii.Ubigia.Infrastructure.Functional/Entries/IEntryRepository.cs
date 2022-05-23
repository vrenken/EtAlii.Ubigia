// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Functional
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IEntryRepository
    {
        /// <summary>
        /// Get the entry for the specified identifier. Only return an entry filled with the specified relations information.
        /// </summary>
        /// <param name="identifier"></param>
        /// <param name="entryRelations"></param>
        /// <returns></returns>
        Task<Entry> Get(Identifier identifier, EntryRelations entryRelations = EntryRelations.None);

        /// <summary>
        /// Get the entries for the specified identifiers. Only return entries filled with the specified relations information.
        /// </summary>
        /// <param name="identifiers"></param>
        /// <param name="entryRelations"></param>
        /// <returns></returns>
        IAsyncEnumerable<Entry> Get(IEnumerable<Identifier> identifiers, EntryRelations entryRelations = EntryRelations.None);

        /// <summary>
        /// Get related entries for the specified identifier. Only return entries filled with the specified relations information.
        /// </summary>
        /// <param name="identifier"></param>
        /// <param name="entriesWithRelation"></param>
        /// <param name="entryRelations"></param>
        /// <returns></returns>
        IAsyncEnumerable<Entry> GetRelated(Identifier identifier, EntryRelations entriesWithRelation, EntryRelations entryRelations = EntryRelations.None);

        /// <summary>
        /// Prepare an entry for storage of information.
        /// </summary>
        /// <param name="spaceId"></param>
        /// <returns></returns>
        Task<Entry> Prepare(Guid spaceId);

        /// <summary>
        /// Prepare an entry for storage of information.
        /// </summary>
        /// <param name="spaceId"></param>
        /// <param name="identifier"></param>
        /// <returns></returns>
        Task<Entry> Prepare(Guid spaceId, Identifier identifier);

        /// <summary>
        /// Store the entry in the backend.
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        Task<Entry> Store(Entry entry);

        /// <summary>
        /// Store the entry in the backend.
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        Task<Entry> Store(IEditableEntry entry);
    }
}
