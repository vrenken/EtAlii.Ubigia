namespace EtAlii.Ubigia.Api.Transport.Diagnostics
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Serilog;

    public class LoggingRootDataClient : IRootDataClient
    {
        private readonly IRootDataClient _client;
        private readonly ILogger _logger = Log.ForContext<IRootDataClient>();

        public LoggingRootDataClient(IRootDataClient client)
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

        public async Task<Root> Add(string name)
        {
            _logger.Information("Adding root (Name: {RootName})", name);
            var start = Environment.TickCount;

            var root = await _client.Add(name).ConfigureAwait(false);

            var duration = TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds;
            _logger.Information("Added root (Name: {RootName} Duration: {Duration}ms)", name, duration);

            return root;
        }

        public async Task Remove(Guid id)
        {
            _logger.Information("Removing root (Id: {RootId})", id);
            var start = Environment.TickCount;

            await _client.Remove(id).ConfigureAwait(false);

            var duration = TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds;
            _logger.Information("Removed root (Id: {RootId} Duration: {Duration}ms)", id, duration);
        }

        public async Task<Root> Change(Guid rootId, string rootName)
        {
            _logger.Information("Changing root (Id: {RootId} Name: {RootName})", rootId, rootName);
            var start = Environment.TickCount;

            var root = await _client.Change(rootId, rootName).ConfigureAwait(false);

            var duration = TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds;
            _logger.Information("Changed root (Id: {RootId} Name: {RootName} Duration: {Duration}ms)", rootId, rootName, duration);

            return root;
        }

        public async Task<Root> Get(string rootName)
        {
            _logger.Information("Getting root (Name: {RootName})", rootName);
            var start = Environment.TickCount;

            var root = await _client.Get(rootName).ConfigureAwait(false);

            var duration = TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds;
            _logger.Information("Got root (Name: {RootName} Duration: {}ms)", rootName, duration);

            return root;
        }

        public async Task<Root> Get(Guid rootId)
        {
            _logger.Information("Getting root (Id: {RootId})", rootId);
            var start = Environment.TickCount;

            var root = await _client.Get(rootId).ConfigureAwait(false);

            var duration = TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds;
            _logger.Information("Got root (Id: {RootId} Duration: {Duration}ms)", rootId, duration);

            return root;
        }

        public async IAsyncEnumerable<Root> GetAll()
        {
            _logger.Information("Getting all roots");
            var start = Environment.TickCount;

            var result = _client.GetAll();
            await foreach (var item in result.ConfigureAwait(false))
            {
                yield return item;
            }

            var duration = TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds;
            _logger.Information("Got all roots (Duration: {Duration}ms)", duration);
        }
    }
}
