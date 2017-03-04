namespace EtAlii.Ubigia.Api.Logical
{
    internal class LogicalContext : ILogicalContext
    {
        public ILogicalContextConfiguration Configuration => _configuration;
        private readonly ILogicalContextConfiguration _configuration;

        public ILogicalNodeSet Nodes => _nodes;
        private readonly ILogicalNodeSet _nodes;

        public ILogicalRootSet Roots => _roots;
        private readonly ILogicalRootSet _roots;

        public IContentManager Content => _content;
        private readonly IContentManager _content;

        public IPropertiesManager Properties => _properties;
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