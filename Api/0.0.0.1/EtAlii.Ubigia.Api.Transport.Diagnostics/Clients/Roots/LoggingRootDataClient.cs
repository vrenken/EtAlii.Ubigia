namespace EtAlii.Ubigia.Api.Transport
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using EtAlii.xTechnology.Logging;

    public class LoggingRootDataClient : IRootDataClient
    {
        private readonly IRootDataClient _client;
        private readonly ILogger _logger;

        public LoggingRootDataClient(
            IRootDataClient client, 
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

        public async Task<Root> Add(string name)
        {
            var message = String.Format("Adding root (Name: {0})", name);
            _logger.Info(message);
            var start = Environment.TickCount;

            var root = await _client.Add(name);

            message = $"Added root (Name: {name} Duration: {TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds}ms)";
            _logger.Info(message);

            return root;
        }

        public async Task Remove(Guid id)
        {
            var message = String.Format("Removing root (Id: {0})", id);
            _logger.Info(message);
            var start = Environment.TickCount;

            await _client.Remove(id);

            message = $"Removed root (Id: {id} Duration: {TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds}ms)";
            _logger.Info(message);
        }

        public async Task<Root> Change(Guid rootId, string rootName)
        {
            var message = String.Format("Changing root (Id: {0} Name: {1})", rootId, rootName);
            _logger.Info(message);
            var start = Environment.TickCount;

            var root = await _client.Change(rootId, rootName);

            message = $"Changed root (Id: {rootId} Name: {rootName} Duration: {TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds}ms)";
            _logger.Info(message);

            return root;
        }

        public async Task<Root> Get(string rootName)
        {
            var message = String.Format("Getting root (Name: {0})", rootName);
            _logger.Info(message);
            var start = Environment.TickCount;

            var root = await _client.Get(rootName);

            message = $"Got root (Name: {rootName} Duration: {TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds}ms)";
            _logger.Info(message);

            return root;
        }

        public async Task<Root> Get(Guid rootId)
        {
            var message = String.Format("Getting root (Id: {0})", rootId);
            _logger.Info(message);
            var start = Environment.TickCount;

            var root = await _client.Get(rootId);

            message = $"Got root (Id: {rootId} Duration: {TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds}ms)";
            _logger.Info(message);

            return root;
        }

        public async Task<IEnumerable<Root>> GetAll()
        {
            var message = String.Format("Getting all roots");
            _logger.Info(message);
            var start = Environment.TickCount;

            var roots = await _client.GetAll();

            message = $"Got all roots (Duration: {TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds}ms)";
            _logger.Info(message);

            return roots;
        }
    }
}
