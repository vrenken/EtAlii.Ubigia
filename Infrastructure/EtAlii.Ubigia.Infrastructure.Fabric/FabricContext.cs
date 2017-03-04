namespace EtAlii.Ubigia.Infrastructure.Fabric
{
    public class FabricContext : IFabricContext
    {
        public IItemsSet Items => _items;
        private readonly IItemsSet _items;

        public IContentSet Content => _content;
        private readonly IContentSet _content;

        public IContentDefinitionSet ContentDefinition => _contentDefinition;
        private readonly IContentDefinitionSet _contentDefinition;

        public IEntrySet Entries => _entries;
        private readonly IEntrySet _entries;

        public IRootSet Roots => _roots;
        private readonly IRootSet _roots;

        public IPropertiesSet Properties => _properties;
        private readonly IPropertiesSet _properties;

        public IIdentifierSet Identifiers => _identifiers;
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