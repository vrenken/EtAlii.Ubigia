// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

// ReSharper disable once CheckNamespace
namespace EtAlii.Ubigia.Infrastructure.Fabric.Diagnostics
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Serilog;

    internal class LoggingEntryStorerDecorator : IEntryStorer
    {
        private readonly ILogger _logger = Log.ForContext<IEntryStorer>();
        private readonly IEntryStorer _decoree;

        public LoggingEntryStorerDecorator(IEntryStorer decoree)
        {
            _decoree = decoree;
        }

        public async Task<(Entry e, IEnumerable<IComponent> storedComponents)> Store(IEditableEntry entry)
        {
            var entryId = entry.Id.ToTimeString();
            _logger.Verbose("Storing entry: {EntryId}", entryId);

            var (e, storedComponents) = await _decoree.Store(entry).ConfigureAwait(false);

            _logger
                .ForContext("StoredComponents", storedComponents)
                .Verbose("Components stored for entry {EntryId}", entryId);

            return (e, storedComponents);
        }

        public async Task<(Entry e, IEnumerable<IComponent> storedComponents)> Store(Entry entry)
        {
            var entryId = entry.Id.ToTimeString();
            _logger.Verbose("Storing entry: {EntryId}", entryId);

            var (e, storedComponents) = await _decoree.Store(entry).ConfigureAwait(false);

            _logger
                .ForContext("StoredComponents", storedComponents)
                .Verbose("Components stored for entry {EntryId}", entryId);

            return (e, storedComponents);
        }
    }
}
