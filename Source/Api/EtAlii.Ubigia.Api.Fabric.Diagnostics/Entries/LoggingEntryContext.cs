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
            var message = "Preparing entry";
            _logger.Information(message);
            var start = Environment.TickCount;

            var result = await _decoree.Prepare();
            
            var duration = TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds;
            _logger.Information("Entry prepared (Duration: {Duration}ms)", duration);
                
            return result;
        }

        public async Task<IReadOnlyEntry> Change(IEditableEntry entry, ExecutionScope scope)
        {
            var message = "Changing entry";
            _logger.Information(message);
            var start = Environment.TickCount;

            var result = await _decoree.Change(entry, scope);
            
            var duration = TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds;
            _logger.Information("Entry changed (Duration: {Duration}ms)", duration);
                
            return result;
        }

        public async Task<IReadOnlyEntry> Get(Root root, ExecutionScope scope)
        {
            var rootName = root?.Name;
            var message = "Retrieving entry for root {Root}";
            _logger.Information(message, rootName);
            var start = Environment.TickCount;

            var result = await _decoree.Get(root, scope);
            
            var duration = TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds;
            _logger.Information("Entry retrieved for root {Root} (Duration: {Duration}ms)", rootName, duration);
                
            return result;
        }

        public async Task<IReadOnlyEntry> Get(Identifier identifier, ExecutionScope scope)
        {
            var identifierTime = identifier.ToTimeString();
            
            var message = "Retrieving entry for identifier {Identifier}";
            _logger.Information(message, identifierTime);
            var start = Environment.TickCount;

            var result = await _decoree.Get(identifier, scope);
            
            var duration = TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds;
            _logger.Information("Entry retrieved for identifier {Identifier} (Duration: {Duration}ms)", identifierTime, duration);
                
            return result;
        }

        public async IAsyncEnumerable<IReadOnlyEntry> Get(IEnumerable<Identifier> identifiers, ExecutionScope scope)
        {
            var message = "Retrieving entries for identifiers";
            _logger.Information(message);
            var start = Environment.TickCount;

            var result = _decoree.Get(identifiers, scope);
            await foreach (var item in result)
            {
                yield return item;
            }

            var duration = TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds;
            _logger.Information("Entries retrieved for identifiers (Duration: {Duration}ms)", duration);
        }

        public async IAsyncEnumerable<IReadOnlyEntry> GetRelated(Identifier identifier, EntryRelation relations, ExecutionScope scope)
        {
            var identifierTime = identifier.ToTimeString();
            
            var message = "Retrieving related entries for identifier {Identifier} (Relation: {Relations})";
            _logger.Information(message, identifierTime, relations);
            var start = Environment.TickCount;

            var result = _decoree.GetRelated(identifier, relations, scope);
            await foreach (var item in result)
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