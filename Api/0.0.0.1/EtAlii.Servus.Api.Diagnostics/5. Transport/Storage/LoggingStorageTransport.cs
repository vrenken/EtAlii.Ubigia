namespace EtAlii.Servus.Api.Management
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Servus.Api.Transport;
    using EtAlii.xTechnology.Logging;
    using EtAlii.xTechnology.MicroContainer;

    public class LoggingStorageTransport : IStorageTransport
    {
        public bool IsConnected { get { return _transport.IsConnected; } }

        private readonly IStorageTransport _transport;
        private readonly ILogger _logger;

        private string _address;

        public LoggingStorageTransport(
            IStorageTransport transport, 
            ILogger logger)
        {
            _transport = transport;
            _logger = logger;
        }

        public void Initialize(IStorageConnection storageConnection, string address)
        {
            _address = address;

            var message = String.Format("Initializing transport (Address: {0})", _address);
            _logger.Info(message);
            var start = Environment.TickCount;

            _transport.Initialize(storageConnection, address);

            message = String.Format("Initialized transport (Address: {0} Duration: {1}ms)", _address, Environment.TickCount - start);
            _logger.Info(message);
        }

        public async Task Start(IStorageConnection storageConnection, string address)
        {
            _address = address;

            var message = String.Format("Starting transport (Address: {0})", _address);
            _logger.Info(message);
            var start = Environment.TickCount;

            await _transport.Start(storageConnection, address);

            message = String.Format("Started transport (Address: {0} Duration: {1}ms)", _address, Environment.TickCount - start);
            _logger.Info(message);
        }

        public async Task Stop(IStorageConnection storageConnection)
        {
            var message = String.Format("Stopping transport (Address: {0})", _address);
            _logger.Info(message);
            var start = Environment.TickCount;

            await _transport.Stop(storageConnection);

            message = String.Format("Stopped transport (Address: {0} Duration: {1}ms)", _address, Environment.TickCount - start);
            _logger.Info(message);
        }

        IScaffolding[] IStorageTransport.CreateScaffolding()
        {
            return _transport.CreateScaffolding();
        }
    }
}
