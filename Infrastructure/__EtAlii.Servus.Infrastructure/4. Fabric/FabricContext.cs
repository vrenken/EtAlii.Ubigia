namespace EtAlii.Servus.Infrastructure.Fabric
{
    public class FabricContext : IFabricContext
    {
        public IItemsSet Items { get { return _items; } }
        private readonly IItemsSet _items;

        public IContentSet Content { get { return _content; } }
        private readonly IContentSet _content;

        public IContentDefinitionSet ContentDefinition { get { return _contentDefinition; } }
        private readonly IContentDefinitionSet _contentDefinition;

        public IEntrySet Entries { get { return _entries; } }
        private readonly IEntrySet _entries;

        public IRootSet Roots { get { return _roots; } }
        private readonly IRootSet _roots;

        public IPropertiesSet Properties { get { return _properties; } }
        private readonly IPropertiesSet _properties;

        public IIdentifierSet Identifiers { get { return _identifiers; } }
        private readonly IIdentifierSet _identifiers;

        public FabricContext(
            IItemsSet items, 
            IContentSet content, 
            IContentDefinitionSet contentDefinition,
            IEntrySet entries,
            IRootSet roots, 
            IPropertiesSet properties, 
            IIdentifierSet identifiers)
        {
            _items = items;
            _content = content;
            _contentDefinition = contentDefinition;
            _roots = roots;
            _properties = properties;
            _identifiers = identifiers;
            _entries = entries;
        }

        public void Start()
        {
            // Not needed (yet).
        }

        public void Stop()
        {
            // Not needed (yet).
        }
    }
}