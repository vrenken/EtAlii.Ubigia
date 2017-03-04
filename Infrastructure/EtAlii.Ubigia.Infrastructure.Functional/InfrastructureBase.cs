namespace EtAlii.Ubigia.Infrastructure.Functional
{
    using EtAlii.Ubigia.Infrastructure.Logical;

    public abstract class InfrastructureBase : IInfrastructure
    {
        public IInfrastructureConfiguration Configuration => _configuration;
        private readonly IInfrastructureConfiguration _configuration;

        public ISpaceRepository Spaces => _spaces;
        private readonly ISpaceRepository _spaces;

        public IIdentifierRepository Identifiers => _identifiers;
        private readonly IIdentifierRepository _identifiers;

        public IEntryRepository Entries => _entries;
        private readonly IEntryRepository _entries;

        public IRootRepository Roots => _roots;
        private readonly IRootRepository _roots;

        public IAccountRepository Accounts => _accounts;
        private readonly IAccountRepository _accounts;

        public IContentRepository Content => _content;
        private readonly IContentRepository _content;

        public IContentDefinitionRepository ContentDefinition => _contentDefinition;
        private readonly IContentDefinitionRepository _contentDefinition;

        public IPropertiesRepository Properties => _properties;
        private readonly IPropertiesRepository _properties;

        public IStorageRepository Storages => _storages;
        private readonly IStorageRepository _storages;

        private readonly ILogicalContext _logicalContext;

        protected InfrastructureBase(
            IInfrastructureConfiguration configuration,
            ISpaceRepository spaces,
            IIdentifierRepository identifiers,
            IEntryRepository entries,
            IRootRepository roots,
            IAccountRepository accounts,
            IContentRepository content,
            IContentDefinitionRepository contentDefinition,
            IPropertiesRepository properties,
            IStorageRepository storages,
            ILogicalContext logicalContext)
        {
            _configuration = configuration;
            _identifiers = identifiers;
            _entries = entries;
            _roots = roots;
            _content = content;
            _contentDefinition = contentDefinition;
            _properties = properties;

            _spaces = spaces;
            _accounts = accounts;
            _storages = storages;
            _logicalContext = logicalContext;
        }

        public virtual void Start()
        {
            _logicalContext.Start();
        }

        public virtual void Stop()
        {
            _logicalContext.Stop();
        }
    }
}