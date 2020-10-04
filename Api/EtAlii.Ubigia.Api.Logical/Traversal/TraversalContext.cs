namespace EtAlii.Ubigia.Api.Logical
{
    public class TraversalContext : ITraversalContext
    {
        public ITraversalContextEntrySet Entries { get; }

        public ITraversalContextRootSet Roots { get; }

        public ITraversalContextPropertySet Properties { get; }

        public TraversalContext(
            ITraversalContextEntrySet entries, 
            ITraversalContextRootSet roots,
            ITraversalContextPropertySet properties)
        {
            Entries = entries;
            Roots = roots;
            Properties = properties;
        }
    }
}
