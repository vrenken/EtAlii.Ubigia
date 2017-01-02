namespace EtAlii.Servus.Api.Transport
{
    using System;
    using EtAlii.xTechnology.Logging;
    using EtAlii.xTechnology.MicroContainer;

    public class LoggingTransport : ITransport
    {
        private readonly ITransport _transport;
        private readonly ILogger _logger;

        private string _address;

        public LoggingTransport(
            ITransport transport, 
            ILogger logger)
        {
            _transport = transport;
            _logger = logger;
        }

        public void Open(string address)
        {
            _address = address;

            var message = String.Format("Opening transport (Address: {0})", _address);
            _logger.Info(message);
            var start = Environment.TickCount;

            _transport.Open(address);

            message = String.Format("Opened transport (Address: {0} Duration: {1}ms)", _address, Environment.TickCount - start);
            _logger.Info(message);

        }

        public void Close()
        {
            var message = String.Format("Closing transport (Address: {0})", _address);
            _logger.Info(message);
            var start = Environment.TickCount;

            _transport.Close();

            message = String.Format("Closed transport (Address: {0} Duration: {1}ms)", _address, Environment.TickCount - start);
            _logger.Info(message);
        }

        public void Register(INotificationClient client)
        {
            var message = String.Format("Registering notification client on transport (Address: {0})", _address);
            _logger.Info(message);
            var start = Environment.TickCount;

            _transport.Register(client);

            message = String.Format("Registered notification client on transport (Address: {0} Duration: {1}ms)", _address, Environment.TickCount - start);
            _logger.Info(message);
        }

        IScaffolding ITransport.CreateScaffolding()
        {
            return _transport.CreateScaffolding();
        }

        public void Register(IDataClient client)
        {
            var message = String.Format("Registering data client on transport (Address: {0})", _address);
            _logger.Info(message);
            var start = Environment.TickCount;

            _transport.Register(client);

            message = String.Format("Registered data client on transport (Address: {0} Duration: {1}ms)", _address, Environment.TickCount - start);
            _logger.Info(message);
        }
    }
}
