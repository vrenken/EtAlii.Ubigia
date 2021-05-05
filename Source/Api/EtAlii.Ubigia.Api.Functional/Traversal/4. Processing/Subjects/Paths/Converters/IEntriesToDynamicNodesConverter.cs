namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Logical;

    public interface IEntriesToDynamicNodesConverter
    {
        IAsyncEnumerable<DynamicNode> Convert(IEnumerable<IReadOnlyEntry> entries, ExecutionScope scope);
        Task<DynamicNode> Convert(IReadOnlyEntry entry, ExecutionScope scope);
    }
}
