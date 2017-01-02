namespace EtAlii.Servus.Api.Data
{
    using System;
    using EtAlii.Servus.Api;
    using System.Collections.Generic;
    using EtAlii.xTechnology.Logging;

    public class LoggingEntryDataClient : IEntryDataClient
    {
        private readonly IEntryDataClient _client;
        private readonly ILogger _logger;


        public LoggingEntryDataClient(
            IEntryDataClient client,
            ILogger logger)
        {
            _client = client;
            _logger = logger;
        }


        public void Connect()
        {
            _client.Connect();
        }

        public void Disconnect()
        {
            _client.Disconnect();
        }

        public IEditableEntry Prepare()
        {
            var message = String.Format("Preparing entry");
            _logger.Info(message);
            var start = Environment.TickCount;

            var entry = _client.Prepare();

            message = String.Format("Prepared entry (Id: {0} Duration: {1}ms)", entry.Id.ToTimeString(), Environment.TickCount - start);
            _logger.Info(message);

            return entry;
        }

        public IReadOnlyEntry Change(IEditableEntry entry)
        {
            var message = String.Format("Changing entry (Id: {0})", entry.Id.ToTimeString());
            _logger.Info(message);
            var start = Environment.TickCount;

            var changedEntry = _client.Change(entry);

            message = String.Format("Changed entry (Id: {0} Duration: {1}ms)", entry.Id.ToTimeString(), Environment.TickCount - start);
            _logger.Info(message);

            return changedEntry;
        }

        public IReadOnlyEntry Get(Root root, EntryRelation entryRelations = EntryRelation.None)
        {
            var message = String.Format("Getting entry (Root: {0})", root.Name);
            _logger.Info(message);
            var start = Environment.TickCount;

            var entry = _client.Get(root, entryRelations);

            message = String.Format("Got entry (Root: {0} Id: {1} Duration: {2}ms)", root.Name, entry.Id.ToTimeString(), Environment.TickCount - start);
            _logger.Info(message);

            return entry;
        }

        public IReadOnlyEntry Get(Identifier entryIdentifier, EntryRelation entryRelations = EntryRelation.None)
        {
            var message = String.Format("Getting entry (Id: {0})", entryIdentifier.ToTimeString());
            _logger.Info(message);
            var start = Environment.TickCount;

            var entry = _client.Get(entryIdentifier, entryRelations);

            message = String.Format("Got entry (Id: {0} Duration: {1}ms)", entryIdentifier.ToTimeString(), Environment.TickCount - start);
            _logger.Info(message);

            return entry;
        }

        public IEnumerable<IReadOnlyEntry> Get(IEnumerable<Identifier> entryIdentifiers, EntryRelation entryRelations = EntryRelation.None)
        {
            var message = String.Format("Getting multiple entries");
            _logger.Info(message);
            var start = Environment.TickCount;

            var entries = _client.Get(entryIdentifiers, entryRelations);

            message = String.Format("Got multiple entries (Duration: {0}ms)", Environment.TickCount - start);
            _logger.Info(message);

            return entries;
        }

        public IEnumerable<IReadOnlyEntry> GetRelated(Identifier entryIdentifier, EntryRelation entriesWithRelation, EntryRelation entryRelations = EntryRelation.None)
        {
            var message = String.Format("Getting related entries");
            _logger.Info(message);
            var start = Environment.TickCount;

            var entries = _client.GetRelated(entryIdentifier, entriesWithRelation, entryRelations);

            message = String.Format("Got related entries (Duration: {0}ms)", Environment.TickCount - start);
            _logger.Info(message);

            return entries;

        }
    }
}
