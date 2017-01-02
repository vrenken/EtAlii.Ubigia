namespace EtAlii.Servus.Api.Transport
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
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


        public async Task Connect(ISpaceConnection spaceConnection)
        {
            await _client.Connect(spaceConnection);
        }

        public async Task Disconnect(ISpaceConnection spaceConnection)
        {
            await _client.Disconnect(spaceConnection);
        }

        public async Task<IEditableEntry> Prepare()
        {
            var message = String.Format("Preparing entry");
            _logger.Info(message);
            var start = Environment.TickCount;

            var entry = await _client.Prepare();

            message = String.Format("Prepared entry (Id: {0} Duration: {1}ms)", entry.Id.ToTimeString(), Environment.TickCount - start);
            _logger.Info(message);

            return entry;
        }

        public async Task<IReadOnlyEntry> Change(IEditableEntry entry, ExecutionScope scope)
        {
            var message = String.Format("Changing entry (Id: {0})", entry.Id.ToTimeString());
            _logger.Info(message);
            var start = Environment.TickCount;

            var changedEntry = await _client.Change(entry, scope);

            message = String.Format("Changed entry (Id: {0} Duration: {1}ms)", entry.Id.ToTimeString(), Environment.TickCount - start);
            _logger.Info(message);

            return changedEntry;
        }

        public async Task<IReadOnlyEntry> Get(Root root, ExecutionScope scope, EntryRelation entryRelations = EntryRelation.None)
        {
            var message = String.Format("Getting entry (Root: {0})", root.Name);
            _logger.Info(message);
            var start = Environment.TickCount;

            var entry = await _client.Get(root, scope, entryRelations);

            message = String.Format("Got entry (Root: {0} Id: {1} Duration: {2}ms)", root.Name, entry.Id.ToTimeString(), Environment.TickCount - start);
            _logger.Info(message);

            return entry;
        }

        public async Task<IReadOnlyEntry> Get(Identifier entryIdentifier, ExecutionScope scope, EntryRelation entryRelations = EntryRelation.None)
        {
            var message = String.Format("Getting entry (Id: {0})", entryIdentifier.ToTimeString());
            _logger.Info(message);
            var start = Environment.TickCount;

            var entry = await _client.Get(entryIdentifier, scope, entryRelations);
    
            message = String.Format("Got entry (Id: {0} Duration: {1}ms)", entryIdentifier.ToTimeString(), Environment.TickCount - start);
            _logger.Info(message);

            return entry;
        }

        public async Task<IEnumerable<IReadOnlyEntry>> Get(IEnumerable<Identifier> entryIdentifiers, ExecutionScope scope, EntryRelation entryRelations = EntryRelation.None)
        {
            var message = String.Format("Getting multiple entries");
            _logger.Info(message);
            var start = Environment.TickCount;

            var entries = await _client.Get(entryIdentifiers, scope, entryRelations);

            message = String.Format("Got multiple entries (Duration: {0}ms)", Environment.TickCount - start);
            _logger.Info(message);

            return entries;
        }

        public async Task<IEnumerable<IReadOnlyEntry>> GetRelated(Identifier entryIdentifier, EntryRelation entriesWithRelation, ExecutionScope scope, EntryRelation entryRelations = EntryRelation.None)
        {
            var message = String.Format("Getting related entries");
            _logger.Info(message);
            var start = Environment.TickCount;

            var entries = await _client.GetRelated(entryIdentifier, entriesWithRelation, scope, entryRelations);

            message = String.Format("Got related entries (Duration: {0}ms)", Environment.TickCount - start);
            _logger.Info(message);

            return entries;

        }
    }
}
