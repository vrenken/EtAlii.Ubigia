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
            IInfrastructureDataContext dataContext,
            IInfrastructureManagementContext managementContext,
            ILogicalContext logicalContext)
        {
            Configuration = configuration;
            Identifiers = dataContext.Identifiers;
            Entries = dataContext.Entries;
            Roots = dataContext.Roots;
            Content = dataContext.Content;
            ContentDefinition = dataContext.ContentDefinition;
            Properties = dataContext.Properties;

            Spaces = managementContext.Spaces;
            Accounts = managementContext.Accounts;
            Storages = managementContext.Storages;
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