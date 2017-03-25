namespace EtAlii.Ubigia.Api.Transport.Diagnostics
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.xTechnology.Logging;
    using EtAlii.xTechnology.MicroContainer;

    public class LoggingSpaceTransport : ISpaceTransport
    {
        public bool IsConnected => _transport.IsConnected;

        private readonly ISpaceTransport _transport;
        private readonly ILogger _logger;

        private string _address;

        public LoggingSpaceTransport(
            ISpaceTransport transport, 
            ILogger logger)
        {
            _transport = transport;
            _logger = logger;
        }

        public void Initialize(ISpaceConnection spaceConnection, string address)
        {
            _address = address;

            var message = $"Initializing transport (Address: {_address})";
            _logger.Info(message);
            var start = Environment.TickCount;

            _transport.Initialize(spaceConnection, address);

            message = $"Initialized transport (Address: {_address} Duration: {TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds}ms)";
            _logger.Info(message);

        }

        public async Task Start(ISpaceConnection spaceConnection, string address)
        {
            _address = address;

            var message = $"Starting transport (Address: {_address})";
            _logger.Info(message);
            var start = Environment.TickCount;

            await _transport.Start(spaceConnection, address);

            message = $"Started transport (Address: {_address} Duration: {TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds}ms)";
            _logger.Info(message);

        }

        public async Task Stop(ISpaceConnection spaceConnection)
        {
            var message = $"Stopping transport (Address: {_address})";
            _logger.Info(message);
            var start = Environment.TickCount;

            await _transport.Stop(spaceConnection);

            message = $"Stopped transport (Address: {_address} Duration: {TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds}ms)";
            _logger.Info(message);
        }

        IScaffolding[] ISpaceTransport.CreateScaffolding()
        {
            return _transport.CreateScaffolding();
        }
    }
}
