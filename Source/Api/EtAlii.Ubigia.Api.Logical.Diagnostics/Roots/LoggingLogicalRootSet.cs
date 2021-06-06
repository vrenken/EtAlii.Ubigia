namespace EtAlii.Ubigia.Api.Logical.Diagnostics
{
    using Serilog;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class LoggingLogicalRootSet : ILogicalRootSet
    {
        private readonly ILogicalRootSet _decoree;
        private readonly ILogger _logger = Log.ForContext<ILogicalRootSet>();

        public event Action<Guid> Added;
        public event Action<Guid> Changed;
        public event Action<Guid> Removed;

        public LoggingLogicalRootSet(ILogicalRootSet decoree)
        {
            _decoree = decoree;
            _decoree.Added += id => Added?.Invoke(id);
            _decoree.Removed += id => Removed?.Invoke(id);
            _decoree.Changed += id => Changed?.Invoke(id);
        }

        public async Task<Root> Add(string name)
        {
            _logger.Information("Adding root {RootName}", name);
            var start = Environment.TickCount;

            var result = await _decoree.Add(name).ConfigureAwait(false);

            var duration = TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds;
            _logger.Information("Added root {RootName} (Duration: {Duration}ms)", result, duration);

            return result;
        }

        public async Task Remove(Guid id)
        {
            _logger.Information("Removing root {RootId}", id);
            var start = Environment.TickCount;

            await _decoree.Remove(id).ConfigureAwait(false);

            var duration = TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds;
            _logger.Information("Removing root (Duration: {Duration}ms)", duration);
        }

        public async Task<Root> Change(Guid rootId, string rootName)
        {
            _logger.Information("Changing root {RootId} {RootName}", rootId, rootName);
            var start = Environment.TickCount;

            var result =await _decoree.Change(rootId, rootName).ConfigureAwait(false);

            var duration = TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds;
            _logger.Information("Changing root (Duration: {Duration}ms)", duration);

            return result;
        }

        public async Task<Root> Get(string rootName)
        {
            _logger.Information("Getting root {RootName}", rootName);
            var start = Environment.TickCount;

            var result = await _decoree.Get(rootName).ConfigureAwait(false);

            var duration = TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds;
            _logger.Information("Got root (Duration: {Duration}ms)", duration);

            return result;
        }

        public async Task<Root> Get(Guid rootId)
        {
            _logger.Information("Getting root {RootId}", rootId);
            var start = Environment.TickCount;

            var result = await _decoree.Get(rootId).ConfigureAwait(false);

            var duration = TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds;
            _logger.Information("Got root (Duration: {Duration}ms)", duration);

            return result;
        }

        public async IAsyncEnumerable<Root> GetAll()
        {
            _logger.Information("Getting all roots");
            var start = Environment.TickCount;

            var items = _decoree.GetAll();
            await foreach (var item in items.ConfigureAwait(false))
            {
                yield return item;
            }

            var duration = TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds;
            _logger.Information("Got all roots (Duration: {Duration}ms)", duration);
        }
    }
}
