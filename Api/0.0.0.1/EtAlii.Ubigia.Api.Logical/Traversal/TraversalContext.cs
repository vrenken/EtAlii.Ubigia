namespace EtAlii.Ubigia.Api.Logical
{
    public class TraversalContext : ITraversalContext
    {
        public ITraversalContextEntrySet Entries => _entries;
        private readonly ITraversalContextEntrySet _entries;

        public ITraversalContextRootSet Roots => _roots;
        private readonly ITraversalContextRootSet _roots;

        public ITraversalContextPropertySet Properties => _properties;
        private readonly ITraversalContextPropertySet _properties;
         
        public TraversalContext(
            ITraversalContextEntrySet entries, 
            ITraversalContextRootSet roots,
            ITraversalContextPropertySet properties)
        {
            _entries = entries;
            _roots = roots;
            _properties = properties;
        }
    }
}
