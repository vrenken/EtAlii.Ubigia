namespace EtAlii.Servus.Api.Fabric
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IEntryContext
    {
        Task<IEditableEntry> Prepare();
        Task<IReadOnlyEntry> Change(IEditableEntry entry, ExecutionScope scope);
        Task<IReadOnlyEntry> Get(Root root, ExecutionScope scope);
        Task<IReadOnlyEntry> Get(Identifier entryIdentifier, ExecutionScope scope);
        Task<IEnumerable<IReadOnlyEntry>> Get(IEnumerable<Identifier> entryIdentifiers, ExecutionScope scope);
        Task<IEnumerable<IReadOnlyEntry>> GetRelated(Identifier entryIdentifier, EntryRelation entriesWithRelation, ExecutionScope scope);

        event Action<Identifier> Prepared;
        event Action<Identifier> Stored;
    }
}
