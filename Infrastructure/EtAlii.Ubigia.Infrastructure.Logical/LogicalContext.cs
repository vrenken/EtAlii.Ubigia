namespace EtAlii.Ubigia.Infrastructure.Logical
{
    using EtAlii.Ubigia.Infrastructure.Fabric;

    public class LogicalContext : ILogicalContext
    {
        public ILogicalStorageSet Storages => _storages;
        private ILogicalStorageSet _storages;

        public ILogicalSpaceSet Spaces => _spaces;
        private ILogicalSpaceSet _spaces;

        public ILogicalAccountSet Accounts => _accounts;
        private ILogicalAccountSet _accounts;

        public ILogicalRootSet Roots => _roots;
        private ILogicalRootSet _roots;

        public ILogicalEntrySet Entries => _entries;
        private ILogicalEntrySet _entries;

        public ILogicalContentSet Content => _content;
        private ILogicalContentSet _content;

        public ILogicalContentDefinitionSet ContentDefinition => _contentDefinition;
        private ILogicalContentDefinitionSet _contentDefinition;

        public ILogicalPropertiesSet Properties => _properties;
        private ILogicalPropertiesSet _properties;

        public ILogicalIdentifierSet Identifiers => _identifiers;
        private ILogicalIdentifierSet _identifiers;

        private readonly IFabricContext _fabricContext;

        public LogicalContext(IFabricContext fabricContext)
        {
            _fabricContext = fabricContext;
        }

        public void Initialize(
            ILogicalStorageSet storages,
            ILogicalSpaceSet spaces,
            ILogicalAccountSet accounts,
            ILogicalRootSet roots,
            ILogicalEntrySet entries,
            ILogicalContentSet content,
            ILogicalContentDefinitionSet contentDefinition,
            ILogicalPropertiesSet properties,
            ILogicalIdentifierSet identifiers)
        {
            _storages = storages;
            _spaces = spaces;
            _accounts = accounts;
            _roots = roots;
            _entries = entries;
            _content = content;
            _contentDefinition = contentDefinition;
            _properties = properties;
            _identifiers = identifiers;
        }

        public void Start()
        {
            _fabricContext.Start();

            _storages.Start();
            _roots.Start();
        }

        public void Stop()
        {
            _roots.Stop();
            _storages.Stop();

            _fabricContext.Stop();
        }
    }
}