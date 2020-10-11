namespace EtAlii.Ubigia.Api.Transport.Diagnostics
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.MicroContainer;

    public class LoggingSpaceTransport : ISpaceTransport
    {
        public bool IsConnected => _transport.IsConnected;

        private readonly ISpaceTransport _transport;
        private readonly ILogger _logger;

        public Uri Address => _transport.Address;

        public LoggingSpaceTransport(
            ISpaceTransport transport, 
            ILogger logger)
        {
            _transport = transport;
            _logger = logger;
        }

        public async Task Start()
        {
            var message = "Starting transport (Address: {address})";
            _logger.Info(message, Address);
            var start = Environment.TickCount;

            await _transport.Start();

            var duration = TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds;
            message = "Started transport (Address: {address} Duration: {duration}ms)";
            _logger.Info(message, Address, duration);

        }

        public async Task Stop()
        {
            var message = "Stopping transport (Address: {address})";
            _logger.Info(message, Address);
            var start = Environment.TickCount;

            await _transport.Stop();

            var duration = TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds;
            message = "Stopped transport (Address: {address} Duration: {duration}ms)";
            _logger.Info(message, Address, duration);
        }

        IScaffolding[] ISpaceTransport.CreateScaffolding()
        {
            return _transport.CreateScaffolding();
        }
    }
}
