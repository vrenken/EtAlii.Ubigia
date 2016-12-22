namespace EtAlii.Servus.Api.Logical
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ITraversalContextEntrySet
    {
        Task<IReadOnlyEntry> Get(Identifier entryIdentifier, ExecutionScope scope);
        Task<IEnumerable<IReadOnlyEntry>> Get(IEnumerable<Identifier> entryIdentifiers, ExecutionScope scope);
        Task<IEnumerable<IReadOnlyEntry>> GetRelated(Identifier entryIdentifier, EntryRelation relation, ExecutionScope scope);
    }
}