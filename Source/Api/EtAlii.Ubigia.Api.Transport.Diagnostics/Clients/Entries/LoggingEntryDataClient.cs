namespace EtAlii.Ubigia.Api.Transport.Diagnostics
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Serilog;

    public class LoggingEntryDataClient : IEntryDataClient
    {
        private readonly IEntryDataClient _client;
        private readonly ILogger _logger = Log.ForContext<IEntryDataClient>();

        public LoggingEntryDataClient(IEntryDataClient client)
        {
            _client = client;
        }


        public async Task Connect(ISpaceConnection spaceConnection)
        {
            await _client.Connect(spaceConnection).ConfigureAwait(false);
        }

        public async Task Disconnect()
        {
            await _client.Disconnect().ConfigureAwait(false);
        }

        public async Task<IEditableEntry> Prepare()
        {
            var message = "Preparing entry";
            _logger.Information(message);
            var start = Environment.TickCount;

            var entry = await _client.Prepare().ConfigureAwait(false);

            var duration = TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds;
            message = "Prepared entry (Id: {EntryId} Duration: {Duration}ms)";
            _logger.Information(message, entry.Id.ToTimeString(), duration);

            return entry;
        }

        public async Task<IReadOnlyEntry> Change(IEditableEntry entry, ExecutionScope scope)
        {
            var message = "Changing entry (Id: {OriginalEntryId})";
            _logger.Information(message, entry.Id.ToTimeString());
            var start = Environment.TickCount;

            var changedEntry = await _client.Change(entry, scope).ConfigureAwait(false);

            var duration = TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds;
            message = "Changed entry (Id: {ChangedEntryId} Duration: {Duration}ms)";
            _logger.Information(message, changedEntry.Id.ToTimeString(), duration);

            return changedEntry;
        }

        public async Task<IReadOnlyEntry> Get(Root root, ExecutionScope scope, EntryRelation entryRelations = EntryRelation.None)
        {
            var message = "Getting entry (Root: {RootName})";
            _logger.Information(message, root.Name);
            var start = Environment.TickCount;

            var entry = await _client.Get(root, scope, entryRelations).ConfigureAwait(false);

            var duration = TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds;
            message = "Got entry (Root: {RootName} Id: {EntryId} Duration: {Duration}ms)";
            _logger.Information(message, root.Name, entry.Id.ToTimeString(), duration);

            return entry;
        }

        public async Task<IReadOnlyEntry> Get(Identifier entryIdentifier, ExecutionScope scope, EntryRelation entryRelations = EntryRelation.None)
        {
            var message = "Getting entry (Id: {EntryId})";
            _logger.Information(message, entryIdentifier.ToTimeString());
            var start = Environment.TickCount;

            var entry = await _client.Get(entryIdentifier, scope, entryRelations).ConfigureAwait(false);

            var duration = TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds;
            message = "Got entry (Id: {EntryId} Duration: {Duration}ms)";
            _logger.Information(message, entryIdentifier.ToTimeString(), duration);

            return entry;
        }

        public async IAsyncEnumerable<IReadOnlyEntry> Get(IEnumerable<Identifier> entryIdentifiers, ExecutionScope scope, EntryRelation entryRelations = EntryRelation.None)
        {
            var message = "Getting multiple entries";
            _logger.Information(message);
            var start = Environment.TickCount;

            var result = _client.Get(entryIdentifiers, scope, entryRelations);
            await foreach (var item in result.ConfigureAwait(false))
            {
                yield return item; 
            }
            
            var duration = TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds;
            message = "Got multiple entries (Duration: {Duration}ms)";
            _logger.Information(message, duration);
        }

        public async IAsyncEnumerable<IReadOnlyEntry> GetRelated(Identifier entryIdentifier, EntryRelation entriesWithRelation, ExecutionScope scope, EntryRelation entryRelations = EntryRelation.None)
        {
            var message = "Getting related entries";
            _logger.Information(message);
            var start = Environment.TickCount;

            var result = _client.GetRelated(entryIdentifier, entriesWithRelation, scope, entryRelations);
            await foreach (var item in result.ConfigureAwait(false))
            {
                yield return item; 
            }

            var duration = TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds;
            message = "Got related entries (Relations: {Relations} Duration: {Duration}ms)";
            _logger.Information(message, entriesWithRelation, duration);

        }
    }
}
