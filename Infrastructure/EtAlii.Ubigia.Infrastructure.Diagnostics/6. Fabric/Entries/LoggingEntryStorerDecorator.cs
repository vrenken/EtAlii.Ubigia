namespace EtAlii.Ubigia.Infrastructure.Diagnostics
{
    using System.Collections.Generic;
    using EtAlii.Ubigia.Infrastructure.Fabric;
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
            return _decoree.Store(entry);
        }

        public Entry Store(Entry entry)
        {
            return _decoree.Store(entry);
        }

        public Entry Store(IEditableEntry entry, out IEnumerable<IComponent> storedComponents)
        {
            return _decoree.Store(entry, out storedComponents);
        }

        public Entry Store(Entry entry, out IEnumerable<IComponent> storedComponents)
        {
            _logger.Verbose("Storing entry: {@entry}", entry);

            entry = _decoree.Store(entry, out storedComponents);

            _logger.Verbose("Components stored: {@components}", storedComponents);

            return entry;
        }
    }
}