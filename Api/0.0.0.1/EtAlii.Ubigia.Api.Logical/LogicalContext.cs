namespace EtAlii.Ubigia.Api.Logical
{
    internal class LogicalContext : ILogicalContext
    {
        public ILogicalContextConfiguration Configuration { get; }

        public ILogicalNodeSet Nodes { get; }

        public ILogicalRootSet Roots { get; }

        public IContentManager Content { get; }

        public IPropertiesManager Properties { get; }

        public LogicalContext(
            ILogicalContextConfiguration configuration,
            ILogicalNodeSet nodes,
            ILogicalRootSet roots, 
            IContentManager content, 
            IPropertiesManager properties)
        {
            Configuration = configuration;
            Nodes = nodes;
            Roots = roots;
            Content = content;
            Properties = properties;
        }

        public void Dispose()
        {
        }
    }
}