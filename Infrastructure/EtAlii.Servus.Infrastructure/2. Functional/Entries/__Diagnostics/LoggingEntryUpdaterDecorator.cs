namespace EtAlii.Servus.Infrastructure
{
    using System.Collections.Generic;
    using EtAlii.Servus.Api;
    using EtAlii.xTechnology.Logging;

    internal class LoggingEntryUpdaterDecorator : IEntryUpdater
    {
        private readonly ILogger _logger;
        private readonly IEntryUpdater _entryUpdater;

        public LoggingEntryUpdaterDecorator(ILogger logger, IEntryUpdater entryUpdater)
        {
            _logger = logger;
            _entryUpdater = entryUpdater;
        }

        public void Update(IEditableEntry entry, IEnumerable<IComponent> changedComponents)
        {
            _entryUpdater.Update(entry, changedComponents);
        }

        public void Update(Entry entry, IEnumerable<IComponent> changedComponents)
        {
            _entryUpdater.Update(entry, changedComponents);
        }
    }
}