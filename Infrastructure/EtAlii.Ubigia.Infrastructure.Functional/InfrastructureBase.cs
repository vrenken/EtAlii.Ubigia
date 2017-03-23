namespace EtAlii.Ubigia.Infrastructure.Functional
{
    using EtAlii.Ubigia.Infrastructure.Logical;

    public abstract class InfrastructureBase : IInfrastructure
    {
        public IInfrastructureConfiguration Configuration { get; }

        public ISpaceRepository Spaces { get; }

        public IIdentifierRepository Identifiers { get; }

        public IEntryRepository Entries { get; }

        public IRootRepository Roots { get; }

        public IAccountRepository Accounts { get; }

        public IContentRepository Content { get; }

        public IContentDefinitionRepository ContentDefinition { get; }

        public IPropertiesRepository Properties { get; }

        public IStorageRepository Storages { get; }

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
            Configuration = configuration;
            Identifiers = identifiers;
            Entries = entries;
            Roots = roots;
            Content = content;
            ContentDefinition = contentDefinition;
            Properties = properties;

            Spaces = spaces;
            Accounts = accounts;
            Storages = storages;
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