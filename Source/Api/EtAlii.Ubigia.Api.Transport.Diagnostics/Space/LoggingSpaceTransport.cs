// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Diagnostics
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.xTechnology.MicroContainer;
    using Serilog;

    public class LoggingSpaceTransport : ISpaceTransport
    {
        public bool IsConnected => _transport.IsConnected;

        private readonly ISpaceTransport _transport;
        private readonly ILogger _logger = Log.ForContext<ISpaceTransport>();

        public Uri Address => _transport.Address;

        public LoggingSpaceTransport(ISpaceTransport transport)
        {
            _transport = transport;
        }

        public async Task Start()
        {
            _logger.Debug("Starting transport (Address: {Address})", Address);
            var start = Environment.TickCount;

            await _transport.Start().ConfigureAwait(false);

            var duration = TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds;
            _logger.Debug("Started transport (Address: {Address} Duration: {Duration}ms)", Address, duration);

        }

        public async Task Stop()
        {
            _logger.Debug("Stopping transport (Address: {Address})", Address);
            var start = Environment.TickCount;

            await _transport.Stop().ConfigureAwait(false);

            var duration = TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds;
            _logger.Debug("Stopped transport (Address: {Address} Duration: {Duration}ms)", Address, duration);
        }

        IScaffolding[] ISpaceTransport.CreateScaffolding()
        {
            return _transport.CreateScaffolding();
        }
    }
}
