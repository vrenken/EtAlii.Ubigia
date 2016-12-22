﻿namespace EtAlii.Servus.Infrastructure.Logical
{
    using EtAlii.Servus.Infrastructure.Fabric;

    public class LogicalContext : ILogicalContext
    {
        public ILogicalStorageSet Storages { get { return _storages; } }
        private ILogicalStorageSet _storages;

        public ILogicalSpaceSet Spaces { get { return _spaces; } }
        private ILogicalSpaceSet _spaces;

        public ILogicalAccountSet Accounts { get { return _accounts; } }
        private ILogicalAccountSet _accounts;

        public ILogicalRootSet Roots { get { return _roots; } }
        private ILogicalRootSet _roots;

        public ILogicalEntrySet Entries { get { return _entries; } }
        private ILogicalEntrySet _entries;

        public ILogicalContentSet Content { get { return _content; } }
        private ILogicalContentSet _content;

        public ILogicalContentDefinitionSet ContentDefinition { get { return _contentDefinition; } }
        private ILogicalContentDefinitionSet _contentDefinition;

        public ILogicalPropertiesSet Properties { get { return _properties; } }
        private ILogicalPropertiesSet _properties;

        public ILogicalIdentifierSet Identifiers { get { return _identifiers; } }
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