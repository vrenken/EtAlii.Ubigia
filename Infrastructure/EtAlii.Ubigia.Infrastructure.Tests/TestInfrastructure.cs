namespace EtAlii.Ubigia.Infrastructure.Tests
{
    using System.Linq;
    using EtAlii.Ubigia.Infrastructure;
    using EtAlii.Ubigia.Infrastructure.Functional;
    using EtAlii.Ubigia.Infrastructure.Logical;
    using EtAlii.Ubigia.Infrastructure.Transport;
    using EtAlii.xTechnology.Logging;
    using Microsoft.Owin.Testing;
    using EtAlii.xTechnology.MicroContainer;

    public class TestInfrastructure : IInfrastructure// WebApiInfrastructure
    {
        public IInfrastructureConfiguration Configuration { get { return _configuration; } }
        private readonly IInfrastructureConfiguration _configuration;

        public ISpaceRepository Spaces { get { return _spaces; } }
        private readonly ISpaceRepository _spaces;

        public IIdentifierRepository Identifiers { get { return _identifiers; } }
        private readonly IIdentifierRepository _identifiers;

        public IEntryRepository Entries { get { return _entries; } }
        private readonly IEntryRepository _entries;

        public IRootRepository Roots { get { return _roots; } }
        private readonly IRootRepository _roots;

        public IAccountRepository Accounts { get { return _accounts; } }
        private readonly IAccountRepository _accounts;

        public IContentRepository Content { get { return _content; } }
        private readonly IContentRepository _content;

        public IContentDefinitionRepository ContentDefinition { get { return _contentDefinition; } }
        private readonly IContentDefinitionRepository _contentDefinition;

        public IPropertiesRepository Properties { get { return _properties; } }
        private readonly IPropertiesRepository _properties;

        public IStorageRepository Storages { get { return _storages; } }
        private readonly IStorageRepository _storages;

        public TestServer Server { get { return _server; } }
        private TestServer _server;

        private readonly Container _container;
        private readonly ILogger _logger;
        private readonly ILogicalContext _logicalContext;
        private IComponentManager[] _componentManagers;

        public TestInfrastructure(
            Container container,
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
            ILogger logger,
            ILogicalContext logicalContext)
        {
            _configuration = configuration;
            _identifiers = identifiers;
            _entries = entries;
            _roots = roots;
            _content = content;
            _contentDefinition = contentDefinition;
            _properties = properties;

            _storages = storages;
            _accounts = accounts;
            _spaces = spaces;

            _container = container;
            _logger = logger;
            _logicalContext = logicalContext;
        }

        public void Start()
        {
            // This action is needed because the Logical layer needs a fully functional system connection to do 
            // the initialization of the storage and spaces.
            // The functional is the only one that can provide these kind of connections.
            _configuration.SystemConnectionCreationProxy.Initialize(() =>
            {
                var configuration = new SystemConnectionConfiguration()
                    .Use(SystemTransportProvider.Create(this))
                    .Use(this);
                return new SystemConnectionFactory().Create(configuration);
            });

            _logicalContext.Start();

            _logger.Info("Starting test infrastructure hosting");

            _componentManagers = _configuration.ComponentManagerFactories
                .Select(componentManagerFactory => componentManagerFactory(_container, _configuration.ComponentFactories))
                .Cast<IComponentManager>()
                .ToArray();

            _server = TestServer.Create(applicationBuilder =>
            {
                foreach (var componentManager in _componentManagers)
                {
                    componentManager.Start(applicationBuilder);
                }
            });

            _logger.Info("Started test infrastructure hosting");
        }

        public void Stop()
        {
            _logger.Info("Stopping test infrastructure hosting");

            _server.Dispose();
            _server = null;

            foreach (var componentManager in _componentManagers)
            {
                componentManager.Stop();
            }

            _logger.Info("Stopped test infrastructure hosting");

            _logicalContext.Stop();
        }
    }
}
