namespace EtAlii.Servus.Api.Data
{
    using EtAlii.Servus.Api;
    using System;
    using System.Collections.Generic;
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

        public void Connect()
        {
            _client.Connect();
        }

        public void Disconnect()
        {
            _client.Disconnect();
        }

        public Root Add(string name)
        {
            var message = String.Format("Adding root (Name: {0})", name);
            _logger.Info(message);
            var start = Environment.TickCount;

            var root = _client.Add(name);

            message = String.Format("Added root (Name: {0} Duration: {1}ms)", name, Environment.TickCount - start);
            _logger.Info(message);

            return root;
        }

        public void Remove(Guid id)
        {
            var message = String.Format("Removing root (Id: {0})", id);
            _logger.Info(message);
            var start = Environment.TickCount;

            _client.Remove(id);

            message = String.Format("Removed root (Id: {0} Duration: {1}ms)", id, Environment.TickCount - start);
            _logger.Info(message);
        }

        public Root Change(Guid rootId, string rootName)
        {
            var message = String.Format("Changing root (Id: {0} Name: {1})", rootId, rootName);
            _logger.Info(message);
            var start = Environment.TickCount;

            var root = _client.Change(rootId, rootName);

            message = String.Format("Changed root (Id: {0} Name: {1} Duration: {2}ms)", rootId, rootName, Environment.TickCount - start);
            _logger.Info(message);

            return root;
        }

        public Root Get(string rootName)
        {
            var message = String.Format("Getting root (Name: {0})", rootName);
            _logger.Info(message);
            var start = Environment.TickCount;

            var root = _client.Get(rootName);

            message = String.Format("Got root (Name: {0} Duration: {1}ms)", rootName, Environment.TickCount - start);
            _logger.Info(message);

            return root;
        }

        public Root Get(Guid rootId)
        {
            var message = String.Format("Getting root (Id: {0})", rootId);
            _logger.Info(message);
            var start = Environment.TickCount;

            var root = _client.Get(rootId);

            message = String.Format("Got root (Id: {0} Duration: {1}ms)", rootId, Environment.TickCount - start);
            _logger.Info(message);

            return root;
        }

        public IEnumerable<Root> GetAll()
        {
            var message = String.Format("Getting all roots");
            _logger.Info(message);
            var start = Environment.TickCount;

            var roots = _client.GetAll();

            message = String.Format("Got all roots (Duration: {0}ms)", Environment.TickCount - start);
            _logger.Info(message);

            return roots;
        }
    }
}
