namespace EtAlii.Servus.Api.Logical
{
    public class TraversalContext : ITraversalContext
    {
        public ITraversalContextEntrySet Entries { get { return _entries; } }
        private readonly ITraversalContextEntrySet _entries;

        public ITraversalContextRootSet Roots { get { return _roots; } }
        private readonly ITraversalContextRootSet _roots;

        public ITraversalContextPropertySet Properties { get{return _properties; } }
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
