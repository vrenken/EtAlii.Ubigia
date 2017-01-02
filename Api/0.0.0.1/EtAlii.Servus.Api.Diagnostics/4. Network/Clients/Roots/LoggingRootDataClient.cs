namespace EtAlii.Servus.Api.Transport
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

            message = String.Format("Added root (Name: {0} Duration: {1}ms)", name, Environment.TickCount - start);
            _logger.Info(message);

            return root;
        }

        public async Task Remove(Guid id)
        {
            var message = String.Format("Removing root (Id: {0})", id);
            _logger.Info(message);
            var start = Environment.TickCount;

            await _client.Remove(id);

            message = String.Format("Removed root (Id: {0} Duration: {1}ms)", id, Environment.TickCount - start);
            _logger.Info(message);
        }

        public async Task<Root> Change(Guid rootId, string rootName)
        {
            var message = String.Format("Changing root (Id: {0} Name: {1})", rootId, rootName);
            _logger.Info(message);
            var start = Environment.TickCount;

            var root = await _client.Change(rootId, rootName);

            message = String.Format("Changed root (Id: {0} Name: {1} Duration: {2}ms)", rootId, rootName, Environment.TickCount - start);
            _logger.Info(message);

            return root;
        }

        public async Task<Root> Get(string rootName)
        {
            var message = String.Format("Getting root (Name: {0})", rootName);
            _logger.Info(message);
            var start = Environment.TickCount;

            var root = await _client.Get(rootName);

            message = String.Format("Got root (Name: {0} Duration: {1}ms)", rootName, Environment.TickCount - start);
            _logger.Info(message);

            return root;
        }

        public async Task<Root> Get(Guid rootId)
        {
            var message = String.Format("Getting root (Id: {0})", rootId);
            _logger.Info(message);
            var start = Environment.TickCount;

            var root = await _client.Get(rootId);

            message = String.Format("Got root (Id: {0} Duration: {1}ms)", rootId, Environment.TickCount - start);
            _logger.Info(message);

            return root;
        }

        public async Task<IEnumerable<Root>> GetAll()
        {
            var message = String.Format("Getting all roots");
            _logger.Info(message);
            var start = Environment.TickCount;

            var roots = await _client.GetAll();

            message = String.Format("Got all roots (Duration: {0}ms)", Environment.TickCount - start);
            _logger.Info(message);

            return roots;
        }
    }
}
