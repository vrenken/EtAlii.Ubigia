namespace EtAlii.Ubigia.Api.Logical
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ITraversalContextEntrySet
    {
        Task<IReadOnlyEntry> Get(Identifier entryIdentifier, ExecutionScope scope);
        Task<IEnumerable<IReadOnlyEntry>> Get(IEnumerable<Identifier> identifiers, ExecutionScope scope); // TODO: Remove this foreach and immediately return an IAsyncEnumerable.
        IAsyncEnumerable<IReadOnlyEntry> GetRelated(Identifier identifier, EntryRelation relation, ExecutionScope scope);
    }
}