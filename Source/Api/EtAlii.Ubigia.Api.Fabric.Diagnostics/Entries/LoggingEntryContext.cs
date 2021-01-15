namespace EtAlii.Ubigia.Api.Fabric.Diagnostics
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Serilog;

    public class LoggingEntryContext : IEntryContext
    {
        private readonly IEntryContext _decoree;
        private readonly ILogger _logger = Log.ForContext<IEntryContext>();

        public LoggingEntryContext(IEntryContext decoree)
        {
            _decoree = decoree;
            _decoree.Prepared += identifier => Prepared?.Invoke(identifier);
            _decoree.Stored += identifier => Stored?.Invoke(identifier);
        }

        public async Task<IEditableEntry> Prepare()
        {
            _logger.Information("Preparing entry");
            var start = Environment.TickCount;

            var result = await _decoree.Prepare().ConfigureAwait(false);

            var duration = TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds;
            _logger.Information("Entry prepared (Duration: {Duration}ms)", duration);

            return result;
        }

        public async Task<IReadOnlyEntry> Change(IEditableEntry entry, ExecutionScope scope)
        {
            _logger.Information("Changing entry");
            var start = Environment.TickCount;

            var result = await _decoree.Change(entry, scope).ConfigureAwait(false);

            var duration = TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds;
            _logger.Information("Entry changed (Duration: {Duration}ms)", duration);

            return result;
        }

        public async Task<IReadOnlyEntry> Get(Root root, ExecutionScope scope)
        {
            var rootName = root?.Name;
            _logger.Information("Retrieving entry for root {Root}", rootName);
            var start = Environment.TickCount;

            var result = await _decoree.Get(root, scope).ConfigureAwait(false);

            var duration = TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds;
            _logger.Information("Entry retrieved for root {Root} (Duration: {Duration}ms)", rootName, duration);

            return result;
        }

        public async Task<IReadOnlyEntry> Get(Identifier identifier, ExecutionScope scope)
        {
            var identifierTime = identifier.ToTimeString();

            _logger.Information("Retrieving entry for identifier {Identifier}", identifierTime);
            var start = Environment.TickCount;

            var result = await _decoree.Get(identifier, scope).ConfigureAwait(false);

            var duration = TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds;
            _logger.Information("Entry retrieved for identifier {Identifier} (Duration: {Duration}ms)", identifierTime, duration);

            return result;
        }

        public async IAsyncEnumerable<IReadOnlyEntry> Get(IEnumerable<Identifier> identifiers, ExecutionScope scope)
        {
            _logger.Information("Retrieving entries for identifiers");
            var start = Environment.TickCount;

            var result = _decoree.Get(identifiers, scope);
            await foreach (var item in result.ConfigureAwait(false))
            {
                yield return item;
            }

            var duration = TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds;
            _logger.Information("Entries retrieved for identifiers (Duration: {Duration}ms)", duration);
        }

        public async IAsyncEnumerable<IReadOnlyEntry> GetRelated(Identifier identifier, EntryRelation relations, ExecutionScope scope)
        {
            var identifierTime = identifier.ToTimeString();

            _logger.Information("Retrieving related entries for identifier {Identifier} (Relation: {Relations})", identifierTime, relations);
            var start = Environment.TickCount;

            var result = _decoree.GetRelated(identifier, relations, scope);
            await foreach (var item in result.ConfigureAwait(false))
            {
                yield return item;
            }

            var duration = TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds;
            _logger.Information("Related entries retrieved for identifier {Identifier} (Relation: {Relations} Duration: {Duration}ms)", identifierTime, relations, duration);
        }

        public event Action<Identifier> Prepared;
        public event Action<Identifier> Stored;
    }
}
