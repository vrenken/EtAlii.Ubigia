namespace EtAlii.Ubigia.Infrastructure.Diagnostics
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Infrastructure.Functional;
    using EtAlii.xTechnology.Logging;

    public class LoggingInfrastructureDecorator : IInfrastructure
    {
        private readonly IInfrastructure _decoree;

        public IInfrastructureConfiguration Configuration => _decoree.Configuration;
        
        public IInformationRepository Information => _decoree.Information;
        public IStorageRepository Storages => _decoree.Storages;
        public ISpaceRepository Spaces => _decoree.Spaces;
        public IIdentifierRepository Identifiers => _decoree.Identifiers;
        public IEntryRepository Entries => _decoree.Entries;
        public IPropertiesRepository Properties => _decoree.Properties;
        public IRootRepository Roots => _decoree.Roots;
        //public IRootInitializer RootInitializer [ get [ return _decoree.RootInitializer; ] ]
        public IAccountRepository Accounts => _decoree.Accounts;
        public IContentRepository Content => _decoree.Content;
        public IContentDefinitionRepository ContentDefinition => _decoree.ContentDefinition;

        private readonly ILogger _logger;

        public LoggingInfrastructureDecorator(
            IInfrastructure decoree, 
            ILogger logger)
        {
            _decoree = decoree;
            _logger = logger;
        }

        public async Task Start()
        {
            _logger.Info("Starting infrastructure hosting");

            await _decoree.Start();

            _logger.Info("Started infrastructure hosting");
        }

        public async Task Stop()
        {
            _logger.Info("Stopping infrastructure hosting");

            await _decoree.Stop();

            _logger.Info("Stopped infrastructure hosting");
        }
    }
}