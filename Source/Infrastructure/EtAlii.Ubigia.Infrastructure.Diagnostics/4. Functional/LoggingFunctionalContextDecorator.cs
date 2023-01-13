// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Diagnostics;

using System.Threading.Tasks;
using EtAlii.Ubigia.Infrastructure.Functional;
using EtAlii.Ubigia.Infrastructure.Logical;
using Serilog;
using EtAlii.xTechnology.Threading;

public sealed class LoggingFunctionalContextDecorator : IFunctionalContext
{
    private readonly IFunctionalContext _decoree;

    /// <inheritdoc />
    public IContextCorrelator ContextCorrelator => _decoree.ContextCorrelator;

    /// <inheritdoc />
    public FunctionalContextOptions Options => _decoree.Options;

    /// <inheritdoc />
    public IInformationRepository Information => _decoree.Information;

    /// <inheritdoc />
    public IStorageRepository Storages => _decoree.Storages;

    /// <inheritdoc />
    public ISpaceRepository Spaces => _decoree.Spaces;

    /// <inheritdoc />
    public IEntryRepository Entries => _decoree.Entries;

    /// <inheritdoc />
    public IPropertiesRepository Properties => _decoree.Properties;

    /// <inheritdoc />
    public IRootRepository Roots => _decoree.Roots;

    /// <inheritdoc />
    public IAccountRepository Accounts => _decoree.Accounts;

    /// <inheritdoc />
    public IContentRepository Content => _decoree.Content;

    /// <inheritdoc />
    public IContentDefinitionRepository ContentDefinition => _decoree.ContentDefinition;

    /// <inheritdoc />
    public ISystemConnectionCreationProxy SystemConnectionCreationProxy => _decoree.SystemConnectionCreationProxy;

    /// <inheritdoc />
    public ILogicalContext LogicalContext => _decoree.LogicalContext;

    /// <inheritdoc />
    public ISystemStatusContext Status => _decoree.Status;

    private readonly ILogger _logger = Log.ForContext<IFunctionalContext>();

    public LoggingFunctionalContextDecorator(IFunctionalContext decoree)
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
