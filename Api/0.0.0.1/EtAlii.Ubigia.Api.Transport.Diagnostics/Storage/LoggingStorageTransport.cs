namespace EtAlii.Ubigia.Api.Transport.Diagnostics
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.xTechnology.Logging;
    using EtAlii.xTechnology.MicroContainer;

    public class LoggingStorageTransport : IStorageTransport
    {
        public bool IsConnected => _transport.IsConnected;

        private readonly IStorageTransport _transport;
        private readonly ILogger _logger;

        private Uri _address;

        public LoggingStorageTransport(
            IStorageTransport transport, 
            ILogger logger)
        {
            _transport = transport;
            _logger = logger;
        }

        public void Initialize(IStorageConnection storageConnection, Uri address)
        {
            _address = address;

            var message = $"Initializing transport (Address: {_address})";
            _logger.Info(message);
            var start = Environment.TickCount;

            _transport.Initialize(storageConnection, address);

            message = $"Initialized transport (Address: {_address} Duration: {TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds}ms)";
            _logger.Info(message);
        }

        public async Task Start(IStorageConnection storageConnection, Uri address)
        {
            _address = address;

            var message = $"Starting transport (Address: {_address})";
            _logger.Info(message);
            var start = Environment.TickCount;

            await _transport.Start(storageConnection, address);

            message = $"Started transport (Address: {_address} Duration: {TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds}ms)";
            _logger.Info(message);
        }

        public async Task Stop(IStorageConnection storageConnection)
        {
            var message = $"Stopping transport (Address: {_address})";
            _logger.Info(message);
            var start = Environment.TickCount;

            await _transport.Stop(storageConnection);

            message = $"Stopped transport (Address: {_address} Duration: {TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds}ms)";
            _logger.Info(message);
        }

        IScaffolding[] IStorageTransport.CreateScaffolding()
        {
            return _transport.CreateScaffolding();
        }
    }
}
