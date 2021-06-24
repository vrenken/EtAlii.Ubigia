// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

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
            _logger.Information("Preparing entry");
            var start = Environment.TickCount;

            var entry = await _client.Prepare().ConfigureAwait(false);

            var duration = TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds;
            _logger.Information("Prepared entry (Id: {EntryId} Duration: {Duration}ms)", entry.Id.ToTimeString(), duration);

            return entry;
        }

        public async Task<IReadOnlyEntry> Change(IEditableEntry entry, ExecutionScope scope)
        {
            _logger.Information("Changing entry (Id: {OriginalEntryId})", entry.Id.ToTimeString());
            var start = Environment.TickCount;

            var changedEntry = await _client.Change(entry, scope).ConfigureAwait(false);

            var duration = TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds;
            _logger.Information("Changed entry (Id: {ChangedEntryId} Duration: {Duration}ms)", changedEntry.Id.ToTimeString(), duration);

            return changedEntry;
        }

        public async Task<IReadOnlyEntry> Get(Root root, ExecutionScope scope, EntryRelation entryRelations = EntryRelation.None)
        {
            _logger.Information("Getting entry (Root: {RootName})", root.Name);
            var start = Environment.TickCount;

            var entry = await _client.Get(root, scope, entryRelations).ConfigureAwait(false);

            var duration = TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds;
            _logger.Information("Got entry (Root: {RootName} Id: {EntryId} Duration: {Duration}ms)", root.Name, entry.Id.ToTimeString(), duration);

            return entry;
        }

        public async Task<IReadOnlyEntry> Get(Identifier entryIdentifier, ExecutionScope scope, EntryRelation entryRelations = EntryRelation.None)
        {
            _logger.Information("Getting entry (Id: {EntryId})", entryIdentifier.ToTimeString());
            var start = Environment.TickCount;

            var entry = await _client.Get(entryIdentifier, scope, entryRelations).ConfigureAwait(false);

            var duration = TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds;
            _logger.Information("Got entry (Id: {EntryId} Duration: {Duration}ms)", entryIdentifier.ToTimeString(), duration);

            return entry;
        }

        public async IAsyncEnumerable<IReadOnlyEntry> Get(IEnumerable<Identifier> entryIdentifiers, ExecutionScope scope, EntryRelation entryRelations = EntryRelation.None)
        {
            _logger.Information("Getting multiple entries");
            var start = Environment.TickCount;

            var result = _client.Get(entryIdentifiers, scope, entryRelations);
            await foreach (var item in result.ConfigureAwait(false))
            {
                yield return item;
            }

            var duration = TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds;
            _logger.Information("Got multiple entries (Duration: {Duration}ms)", duration);
        }

        public async IAsyncEnumerable<IReadOnlyEntry> GetRelated(Identifier entryIdentifier, EntryRelation entriesWithRelation, ExecutionScope scope, EntryRelation entryRelations = EntryRelation.None)
        {
            _logger.Information("Getting related entries");
            var start = Environment.TickCount;

            var result = _client.GetRelated(entryIdentifier, entriesWithRelation, scope, entryRelations);
            await foreach (var item in result.ConfigureAwait(false))
            {
                yield return item;
            }

            var duration = TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds;
            _logger.Information("Got related entries (Relations: {Relations} Duration: {Duration}ms)", entriesWithRelation, duration);

        }
    }
}
