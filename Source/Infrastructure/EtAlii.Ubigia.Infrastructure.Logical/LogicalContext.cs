// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Logical;

using System.Threading.Tasks;
using EtAlii.Ubigia.Infrastructure.Fabric;

public class LogicalContext : ILogicalContext
{
    /// <inheritdoc cref="ILogicalContext"/>
    public ILogicalStorageSet Storages { get; private set; }

    /// <inheritdoc cref="ILogicalContext"/>
    public ILogicalSpaceSet Spaces { get; private set; }

    /// <inheritdoc cref="ILogicalContext"/>
    public ILogicalAccountSet Accounts { get; private set; }

    /// <inheritdoc cref="ILogicalContext"/>
    public ILogicalRootSet Roots { get; private set; }

    /// <inheritdoc cref="ILogicalContext"/>
    public ILogicalEntrySet Entries { get; private set; }

    /// <inheritdoc cref="ILogicalContext"/>
    public ILogicalContentSet Content { get; private set; }

    /// <inheritdoc cref="ILogicalContext"/>
    public ILogicalContentDefinitionSet ContentDefinition { get; private set; }

    /// <inheritdoc cref="ILogicalContext"/>
    public ILogicalPropertiesSet Properties { get; private set; }

    /// <inheritdoc cref="ILogicalContext"/>
    public ILogicalIdentifierSet Identifiers { get; private set; }

    private readonly IFabricContext _fabricContext;

    public LogicalContext(IFabricContext fabricContext)
    {
        _fabricContext = fabricContext;
    }

    /// <inheritdoc cref="ILogicalContext"/>
    public void Initialize(
        ILogicalStorageSet storages,
        ILogicalSpaceSet spaces,
        ILogicalAccountSet accounts,
        ILogicalRootSet roots,
        ILogicalEntrySet entries,
        ILogicalContentSet content,
        ILogicalContentDefinitionSet contentDefinition,
        ILogicalPropertiesSet properties,
        ILogicalIdentifierSet identifiers)
    {
        Storages = storages;
        Spaces = spaces;
        Accounts = accounts;
        Roots = roots;
        Entries = entries;
        Content = content;
        ContentDefinition = contentDefinition;
        Properties = properties;
        Identifiers = identifiers;
    }

    /// <inheritdoc cref="ILogicalContext"/>
    public async Task Start()
    {
        _fabricContext.Start();

        await Storages.Start().ConfigureAwait(false);
        Roots.Start();
    }

    /// <inheritdoc cref="ILogicalContext"/>
    public async Task Stop()
    {
        Roots.Stop();
        await Storages.Stop().ConfigureAwait(false);

        _fabricContext.Stop();
    }
}
