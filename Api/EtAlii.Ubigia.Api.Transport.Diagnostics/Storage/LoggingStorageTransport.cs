namespace EtAlii.Ubigia.Api.Transport.Diagnostics
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.MicroContainer;

    public class LoggingStorageTransport : IStorageTransport
    {
        public bool IsConnected => _transport.IsConnected;

        private readonly IStorageTransport _transport;
        private readonly ILogger _logger;

        public Uri Address => _transport.Address;

        public LoggingStorageTransport(
            IStorageTransport transport, 
            ILogger logger)
        {
            _transport = transport;
            _logger = logger;
        }

        public async Task Start()
        {
            var message = $"Starting transport (Address: {Address})";
            _logger.Info(message);
            var start = Environment.TickCount;

            await _transport.Start();

            message = $"Started transport (Address: {Address} Duration: {TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds}ms)";
            _logger.Info(message);
        }

        public async Task Stop()
        {
            var message = $"Stopping transport (Address: {Address})";
            _logger.Info(message);
            var start = Environment.TickCount;

            await _transport.Stop();

            message = $"Stopped transport (Address: {Address} Duration: {TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds}ms)";
            _logger.Info(message);
        }

        IScaffolding[] IStorageTransport.CreateScaffolding()
        {
            return _transport.CreateScaffolding();
        }
    }
}
