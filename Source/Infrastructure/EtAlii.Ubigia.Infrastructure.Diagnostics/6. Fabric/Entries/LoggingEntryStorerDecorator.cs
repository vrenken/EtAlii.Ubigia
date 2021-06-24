// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

// ReSharper disable once CheckNamespace
namespace EtAlii.Ubigia.Infrastructure.Fabric.Diagnostics
{
    using System.Collections.Generic;
    using Serilog;

    internal class LoggingEntryStorerDecorator : IEntryStorer
    {
        private readonly ILogger _logger = Log.ForContext<IEntryStorer>();
        private readonly IEntryStorer _decoree;

        public LoggingEntryStorerDecorator(IEntryStorer decoree)
        {
            _decoree = decoree;
        }

        public Entry Store(IEditableEntry entry)
        {
            _logger.Verbose("Storing entry: {EntryId}", entry.Id.ToTimeString());

            return _decoree.Store(entry);
        }

        public Entry Store(Entry entry)
        {
            _logger.Verbose("Storing entry: {EntryId}", entry.Id.ToTimeString());

            return _decoree.Store(entry);
        }

        public Entry Store(IEditableEntry entry, out IEnumerable<IComponent> storedComponents)
        {
            var entryId = entry.Id.ToTimeString();
            _logger.Verbose("Storing entry: {EntryId}", entryId);

            var result = _decoree.Store(entry, out storedComponents);

            _logger
                .ForContext("StoredComponents", storedComponents)
                .Verbose("Components stored for entry {EntryId}", entryId);

            return result;
        }

        public Entry Store(Entry entry, out IEnumerable<IComponent> storedComponents)
        {
            var entryId = entry.Id.ToTimeString();
            _logger.Verbose("Storing entry: {EntryId}", entryId);

            var result = _decoree.Store(entry, out storedComponents);

            _logger
                .ForContext("StoredComponents", storedComponents)
                .Verbose("Components stored for entry {EntryId}", entryId);

            return result;
        }
    }
}
