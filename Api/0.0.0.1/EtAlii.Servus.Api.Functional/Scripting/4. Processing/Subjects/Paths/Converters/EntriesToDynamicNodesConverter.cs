namespace EtAlii.Servus.Api.Functional
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using EtAlii.Servus.Api.Logical;

    public class EntriesToDynamicNodesConverter : IEntriesToDynamicNodesConverter
    {
        private readonly IProcessingContext _context;

        public EntriesToDynamicNodesConverter(IProcessingContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DynamicNode>> Convert(IEnumerable<IReadOnlyEntry> entries, ExecutionScope scope)
        {
            var result = new List<DynamicNode>();

            foreach (var entry in entries)
            {
                // We want retrieved nodes to have the properties assigned, right?
                var properties = await _context.Logical.Properties.Get(entry.Id, scope) ?? new PropertyDictionary();
                result.Add(new DynamicNode(entry, properties));
            }

            return result.ToArray();
        }

        public async Task<DynamicNode> Convert(IReadOnlyEntry entry, ExecutionScope scope)
        {
            // We want retrieved nodes to have the properties assigned, right?
            var properties = await _context.Logical.Properties.Get(entry.Id, scope) ?? new PropertyDictionary();
            return new DynamicNode(entry, properties);
        }
    }
}