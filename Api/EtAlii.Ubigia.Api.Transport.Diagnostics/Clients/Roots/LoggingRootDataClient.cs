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
            await _client.Connect(spaceConnection);
        }

        public async Task Disconnect()
        {
            await _client.Disconnect();
        }

        public async Task<Root> Add(string name)
        {
            var message = "Adding root (Name: {RootName})";
            _logger.Information(message, name);
            var start = Environment.TickCount;

            var root = await _client.Add(name);

            var duration = TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds;
            message = "Added root (Name: {RootName} Duration: {Duration}ms)";
            _logger.Information(message, name, duration);

            return root;
        }

        public async Task Remove(Guid id)
        {
            var message = "Removing root (Id: {RootId})";
            _logger.Information(message, id);
            var start = Environment.TickCount;

            await _client.Remove(id);

            var duration = TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds;
            message = "Removed root (Id: {RootId} Duration: {Duration}ms)";
            _logger.Information(message, id, duration);
        }

        public async Task<Root> Change(Guid rootId, string rootName)
        {
            var message = "Changing root (Id: {RootId} Name: {RootName})";
            _logger.Information(message, rootId, rootName);
            var start = Environment.TickCount;

            var root = await _client.Change(rootId, rootName);

            var duration = TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds;
            message = "Changed root (Id: {RootId} Name: {RootName} Duration: {Duration}ms)";
            _logger.Information(message, rootId, rootName, duration);

            return root;
        }

        public async Task<Root> Get(string rootName)
        {
            var message = "Getting root (Name: {RootName})";
            _logger.Information(message, rootName);
            var start = Environment.TickCount;

            var root = await _client.Get(rootName);

            var duration = TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds;
            message = "Got root (Name: {RootName} Duration: {}ms)";
            _logger.Information(message, rootName, duration);

            return root;
        }

        public async Task<Root> Get(Guid rootId)
        {
            var message = "Getting root (Id: {RootId})";
            _logger.Information(message, rootId);
            var start = Environment.TickCount;

            var root = await _client.Get(rootId);

            var duration = TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds;
            message = "Got root (Id: {RootId} Duration: {Duration}ms)";
            _logger.Information(message, rootId, duration);

            return root;
        }

        public async Task<IEnumerable<Root>> GetAll()
        {
            var message = "Getting all roots";
            _logger.Information(message);
            var start = Environment.TickCount;

            var roots = await _client.GetAll();

            var duration = TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds;
            message = "Got all roots (Duration: {Duration}ms)";
            _logger.Information(message, duration);

            return roots;
        }
    }
}
