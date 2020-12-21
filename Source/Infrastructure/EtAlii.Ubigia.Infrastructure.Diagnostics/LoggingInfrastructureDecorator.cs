namespace EtAlii.Ubigia.Infrastructure.Diagnostics
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Infrastructure.Functional;
    using Serilog;
    using EtAlii.xTechnology.Threading;

    public sealed class LoggingInfrastructureDecorator : IInfrastructure
    {
        private readonly IInfrastructure _decoree;

        public IContextCorrelator ContextCorrelator => _decoree.ContextCorrelator;
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

        private readonly ILogger _logger = Log.ForContext<IInfrastructure>();

        public LoggingInfrastructureDecorator(IInfrastructure decoree)
        {
            _decoree = decoree;
        }

        public async Task Start()
        {
            _logger.Information("Starting infrastructure hosting");

            await _decoree.Start().ConfigureAwait(false);

            _logger.Information("Started infrastructure hosting");
        }

        public async Task Stop()
        {
            _logger.Information("Stopping infrastructure hosting");

            await _decoree.Stop().ConfigureAwait(false);

            _logger.Information("Stopped infrastructure hosting");
        }
    }
}
