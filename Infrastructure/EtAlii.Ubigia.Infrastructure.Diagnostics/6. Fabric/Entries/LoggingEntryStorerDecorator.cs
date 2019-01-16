namespace EtAlii.Ubigia.Infrastructure.Diagnostics
{
    using System.Collections.Generic;
    using System.Linq;
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.Infrastructure.Fabric;
    using EtAlii.xTechnology.Logging;

    internal class LoggingEntryStorerDecorator : IEntryStorer
    {
        private readonly ILogger _logger;
        private readonly IEntryStorer _entryStorer;

        public LoggingEntryStorerDecorator(ILogger logger, IEntryStorer entryStorer)
        {
            _logger = logger;
            _entryStorer = entryStorer;
        }

        public Entry Store(IEditableEntry entry)
        {
            return _entryStorer.Store(entry);
        }

        public Entry Store(Entry entry)
        {
            return _entryStorer.Store(entry);
        }

        public Entry Store(IEditableEntry entry, out IEnumerable<IComponent> storedComponents)
        {
            return _entryStorer.Store(entry, out storedComponents);
        }

        public Entry Store(Entry entry, out IEnumerable<IComponent> storedComponents)
        {
            _logger.Verbose("Storing entry: {0} (Components to store: {1})", entry.Id.ToTimeString());

            entry = _entryStorer.Store(entry, out storedComponents);

            _logger.Verbose("Components stored: {0}", storedComponents.Count());

            return entry;
        }
    }
}