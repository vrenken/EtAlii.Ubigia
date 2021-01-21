namespace EtAlii.Ubigia.Api.Logical
{
    public class PathTraversalContext : IPathTraversalContext
    {
        public ITraversalContextEntrySet Entries { get; }

        public ITraversalContextRootSet Roots { get; }

        public ITraversalContextPropertySet Properties { get; }

        public PathTraversalContext(
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
