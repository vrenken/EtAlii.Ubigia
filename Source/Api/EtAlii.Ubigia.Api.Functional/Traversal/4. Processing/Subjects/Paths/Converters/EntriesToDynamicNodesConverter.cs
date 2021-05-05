namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Logical;

    public class EntriesToDynamicNodesConverter : IEntriesToDynamicNodesConverter
    {
        private readonly IScriptProcessingContext _context;

        public EntriesToDynamicNodesConverter(IScriptProcessingContext context)
        {
            _context = context;
        }

        public async IAsyncEnumerable<DynamicNode> Convert(IEnumerable<IReadOnlyEntry> entries, ExecutionScope scope)
        {
            foreach (var entry in entries)
            {
                // We want retrieved nodes to have the properties assigned, right?
                var properties = await _context.Logical.Properties.Get(entry.Id, scope).ConfigureAwait(false) ?? new PropertyDictionary();
                yield return new DynamicNode(entry, properties);
            }
        }

        public async Task<DynamicNode> Convert(IReadOnlyEntry entry, ExecutionScope scope)
        {
            // We want retrieved nodes to have the properties assigned, right?
            var properties = await _context.Logical.Properties.Get(entry.Id, scope).ConfigureAwait(false) ?? new PropertyDictionary();
            return new DynamicNode(entry, properties);
        }
    }
}
