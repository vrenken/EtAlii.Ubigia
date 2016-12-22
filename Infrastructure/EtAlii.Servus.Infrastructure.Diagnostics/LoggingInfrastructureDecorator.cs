namespace EtAlii.Servus.Infrastructure
{
    using EtAlii.Servus.Infrastructure.Functional;
    using EtAlii.xTechnology.Logging;

    public class LoggingInfrastructureDecorator : IInfrastructure
    {
        private readonly IInfrastructure _decoree;

        public IInfrastructureConfiguration Configuration { get { return _decoree.Configuration; } }
        public IStorageRepository Storages { get { return _decoree.Storages; } }
        public ISpaceRepository Spaces { get { return _decoree.Spaces; } }
        public IIdentifierRepository Identifiers { get { return _decoree.Identifiers; } }
        public IEntryRepository Entries { get { return _decoree.Entries; } }
        public IPropertiesRepository Properties { get { return _decoree.Properties; } }
        public IRootRepository Roots { get { return _decoree.Roots; } }
        //public IRootInitializer RootInitializer { get { return _decoree.RootInitializer; } }
        public IAccountRepository Accounts { get { return _decoree.Accounts; } }
        public IContentRepository Content { get { return _decoree.Content; } }
        public IContentDefinitionRepository ContentDefinition { get { return _decoree.ContentDefinition; } }

        private readonly ILogger _logger;

        public LoggingInfrastructureDecorator(
            IInfrastructure decoree, 
            ILogger logger)
        {
            _decoree = decoree;
            _logger = logger;
        }

        public void Start()
        {
            _logger.Info("Starting infrastructure hosting");

            _decoree.Start();

            _logger.Info("Started infrastructure hosting");
        }

        public void Stop()
        {
            _logger.Info("Stopping infrastructure hosting");

            _decoree.Stop();

            _logger.Info("Stopped infrastructure hosting");
        }
    }
}