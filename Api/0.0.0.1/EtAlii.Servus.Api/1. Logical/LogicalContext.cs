namespace EtAlii.Servus.Api.Logical
{
    internal class LogicalContext : ILogicalContext
    {
        public ILogicalContextConfiguration Configuration { get { return _configuration; } }
        private readonly ILogicalContextConfiguration _configuration;

        public ILogicalNodeSet Nodes { get { return _nodes; } }
        private readonly ILogicalNodeSet _nodes;

        public ILogicalRootSet Roots { get { return _roots; } }
        private readonly ILogicalRootSet _roots;

        public IContentManager Content { get { return _content; } }
        private readonly IContentManager _content;

        public IPropertiesManager Properties { get { return _properties; } }
        private readonly IPropertiesManager _properties;

        public LogicalContext(
            ILogicalContextConfiguration configuration,
            ILogicalNodeSet nodes,
            ILogicalRootSet roots, 
            IContentManager content, 
            IPropertiesManager properties)
        {
            _configuration = configuration;
            _nodes = nodes;
            _roots = roots;
            _content = content;
            _properties = properties;
        }

        public void Dispose()
        {
        }
    }
}