namespace EtAlii.Ubigia.Api.Transport.Diagnostics
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.xTechnology.MicroContainer;
    using Serilog;

    public class LoggingStorageTransport : IStorageTransport
    {
        public bool IsConnected => _transport.IsConnected;

        private readonly IStorageTransport _transport;
        private readonly ILogger _logger = Log.ForContext<IStorageTransport>();

        public Uri Address => _transport.Address;

        public LoggingStorageTransport(IStorageTransport transport)
        {
            _transport = transport;
        }

        public async Task Start()
        {
            var message = "Starting transport (Address: {address})";
            _logger.Information(message, Address);
            var start = Environment.TickCount;

            await _transport.Start();

            var duration = TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds;
            message = "Started transport (Address: {address} Duration: {duration}ms)";
            _logger.Information(message, Address, duration);
        }

        public async Task Stop()
        {
            var message = "Stopping transport (Address: {address})";
            _logger.Information(message, Address);
            var start = Environment.TickCount;

            await _transport.Stop();

            var duration = TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds;
            message = "Stopped transport (address: {address} Duration: {duration}ms)";
            _logger.Information(message, Address, duration);
        }

        IScaffolding[] IStorageTransport.CreateScaffolding()
        {
            return _transport.CreateScaffolding();
        }
    }
}
