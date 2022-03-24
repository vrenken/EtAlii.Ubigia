// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Fabric.Diagnostics
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Serilog;

    public sealed class LoggingEntryContext : IEntryContext
    {
        private readonly IEntryContext _decoree;
        private readonly ILogger _logger = Log.ForContext<IEntryContext>();

        public LoggingEntryContext(IEntryContext decoree)
        {
            _decoree = decoree;
        }

        public async Task<IEditableEntry> Prepare()
        {
            _logger.Debug("Preparing entry");
            var start = Environment.TickCount;

            var result = await _decoree.Prepare().ConfigureAwait(false);

            var duration = TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds;
            _logger.Debug("Entry prepared (Duration: {Duration}ms)", duration);

            return result;
        }

        public async Task<IReadOnlyEntry> Change(IEditableEntry entry, ExecutionScope scope)
        {
            _logger.Debug("Changing entry");
            var start = Environment.TickCount;

            var result = await _decoree.Change(entry, scope).ConfigureAwait(false);

            var duration = TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds;
            _logger.Debug("Entry changed (Duration: {Duration}ms)", duration);

            return result;
        }

        public async Task<IReadOnlyEntry> Get(Root root, ExecutionScope scope)
        {
            var rootName = root?.Name;
            _logger.Debug("Retrieving entry for root {Root}", rootName);
            var start = Environment.TickCount;

            var result = await _decoree.Get(root, scope).ConfigureAwait(false);

            var duration = TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds;
            _logger.Debug("Entry retrieved for root {Root} (Duration: {Duration}ms)", rootName, duration);

            return result;
        }

        public async Task<IReadOnlyEntry> Get(Identifier identifier, ExecutionScope scope)
        {
            var identifierTime = identifier.ToTimeString();

            _logger.Debug("Retrieving entry for identifier {Identifier}", identifierTime);
            var start = Environment.TickCount;

            var result = await _decoree.Get(identifier, scope).ConfigureAwait(false);

            var duration = TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds;
            _logger.Debug("Entry retrieved for identifier {Identifier} (Duration: {Duration}ms)", identifierTime, duration);

            return result;
        }

        public async IAsyncEnumerable<IReadOnlyEntry> Get(IEnumerable<Identifier> identifiers, ExecutionScope scope)
        {
            _logger.Debug("Retrieving entries for identifiers");
            var start = Environment.TickCount;

            var result = _decoree
                .Get(identifiers, scope)
                .ConfigureAwait(false);
            await foreach (var item in result)
            {
                yield return item;
            }

            var duration = TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds;
            _logger.Debug("Entries retrieved for identifiers (Duration: {Duration}ms)", duration);
        }

        public async IAsyncEnumerable<IReadOnlyEntry> GetRelated(Identifier identifier, EntryRelations relations, ExecutionScope scope)
        {
            var identifierTime = identifier.ToTimeString();

            _logger.Debug("Retrieving related entries for identifier {Identifier} (Relation: {Relations})", identifierTime, relations);
            var start = Environment.TickCount;

            var result = _decoree
                .GetRelated(identifier, relations, scope)
                .ConfigureAwait(false);
            await foreach (var item in result)
            {
                yield return item;
            }

            var duration = TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds;
            _logger.Debug("Related entries retrieved for identifier {Identifier} (Relation: {Relations} Duration: {Duration}ms)", identifierTime, relations, duration);
        }
    }
}
