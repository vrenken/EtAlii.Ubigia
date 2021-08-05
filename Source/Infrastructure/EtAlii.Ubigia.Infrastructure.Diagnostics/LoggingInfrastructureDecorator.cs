// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Diagnostics
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Infrastructure.Functional;
    using Serilog;
    using EtAlii.xTechnology.Threading;

    public sealed class LoggingInfrastructureDecorator : IInfrastructure
    {
        private readonly IInfrastructure _decoree;

        /// <inheritdoc />
        public IContextCorrelator ContextCorrelator => _decoree.ContextCorrelator;

        /// <inheritdoc />
        public IInfrastructureOptions Options => _decoree.Options;

        /// <inheritdoc />
        public IInformationRepository Information => _decoree.Information;

        /// <inheritdoc />
        public IStorageRepository Storages => _decoree.Storages;

        /// <inheritdoc />
        public ISpaceRepository Spaces => _decoree.Spaces;

        /// <inheritdoc />
        public IIdentifierRepository Identifiers => _decoree.Identifiers;

        /// <inheritdoc />
        public IEntryRepository Entries => _decoree.Entries;

        /// <inheritdoc />
        public IPropertiesRepository Properties => _decoree.Properties;

        /// <inheritdoc />
        public IRootRepository Roots => _decoree.Roots;
        //public IRootInitializer RootInitializer [ get [ return _decoree.RootInitializer; ] ]

        /// <inheritdoc />
        public IAccountRepository Accounts => _decoree.Accounts;

        /// <inheritdoc />
        public IContentRepository Content => _decoree.Content;

        /// <inheritdoc />
        public IContentDefinitionRepository ContentDefinition => _decoree.ContentDefinition;

        private readonly ILogger _logger = Log.ForContext<IInfrastructure>();

        public LoggingInfrastructureDecorator(IInfrastructure decoree)
        {
            _decoree = decoree;
        }

        /// <inheritdoc />
        public async Task Start()
        {
            _logger.Information("Starting infrastructure hosting");

            await _decoree.Start().ConfigureAwait(false);

            _logger.Information("Started infrastructure hosting");
        }

        /// <inheritdoc />
        public async Task Stop()
        {
            _logger.Information("Stopping infrastructure hosting");

            await _decoree.Stop().ConfigureAwait(false);

            _logger.Information("Stopped infrastructure hosting");
        }
    }
}
