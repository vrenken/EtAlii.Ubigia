namespace EtAlii.Servus.Infrastructure.Hosting
{
    using System;
    using EtAlii.Servus.Infrastructure;
    using EtAlii.Servus.Storage;
    using EtAlii.xTechnology.Logging;
    using Microsoft.Owin.Hosting;
    using SimpleInjector;

    public class WebApiInfrastructure : IInfrastructure
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

        public IRootInitializer RootInitializer { get { return _rootInitializer; } }
        private readonly IRootInitializer _rootInitializer;

        public IAccountRepository Accounts { get { return _accounts; } }
        private readonly IAccountRepository _accounts;

        public IContentRepository Content { get { return _content; } }
        private readonly IContentRepository _content;

        public IContentDefinitionRepository ContentDefinition { get { return _contentDefinition; } }
        private readonly IContentDefinitionRepository _contentDefinition;

        public IStorageRepository Storages { get { return _storages; } }
        private readonly IStorageRepository _storages;

        internal IStorage Storage { get { return _storage; } }
        private readonly IStorage _storage;

        private ILogger _logger;
        private Container _container;
        private IComponentManager _componentManager;
        private IDisposable _host;

        public WebApiInfrastructure(
            Container container,
            IInfrastructureConfiguration configuration,
            ISpaceRepository spaces,
            IIdentifierRepository identifiers,
            IEntryRepository entries,
            IRootRepository roots,
            IRootInitializer rootInitializer,
            IAccountRepository accounts,
            IContentRepository content,
            IContentDefinitionRepository contentDefinition,
            IStorageRepository storages,
            IStorage storage,
            ILogger logger)
        {
            _configuration = configuration;
            _spaces = spaces;
            _identifiers = identifiers;
            _entries = entries;
            _roots = roots;
            _rootInitializer = rootInitializer;
            _accounts = accounts;
            _content = content;
            _contentDefinition = contentDefinition;
            _storages = storages;
            _storage = storage;
            _container = container;
            _logger = logger;
        }

        public void Start()
        {
            _logger.Info("Starting test infrastructure hosting");

            _componentManager = _container.GetInstance<IComponentManager>();
            _host = WebApp.Start(_configuration.Address, _componentManager.Start);

            _logger.Info("Started infrastructure hosting");
        }

        public void Stop()
        {
            _logger.Info("Stopping infrastructure hosting");

            if (_host != null)
            {
                _host.Dispose();
                _host = null;
            }

            if (_componentManager != null)
            {
                _componentManager.Stop();
                _componentManager = null;
            }

            _logger.Info("Stopped infrastructure hosting");
        }

    }
}