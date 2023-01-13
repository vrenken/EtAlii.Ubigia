// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

// ReSharper disable once CheckNamespace
namespace EtAlii.Ubigia.Infrastructure.Fabric.Diagnostics;

using System.Collections.Generic;
using System.Threading.Tasks;
using Serilog;

internal class LoggingEntryUpdaterDecorator : IEntryUpdater
{
    private readonly ILogger _logger = Log.ForContext<IEntryUpdater>();
    private readonly IEntryUpdater _entryUpdater;

    public LoggingEntryUpdaterDecorator(IEntryUpdater entryUpdater)
    {
        _entryUpdater = entryUpdater;
    }

    public Task Update(IEditableEntry entry, IEnumerable<IComponent> changedComponents)
    {
        _logger.Verbose("Updating entry: {Identifier}", entry.Id.ToTimeString());

        return _entryUpdater.Update(entry, changedComponents);
    }

    public Task Update(Entry entry, IEnumerable<IComponent> changedComponents)
    {
        _logger.Verbose("Updating entry: {Identifier}", entry.Id.ToTimeString());

        return _entryUpdater.Update(entry, changedComponents);
    }
}
