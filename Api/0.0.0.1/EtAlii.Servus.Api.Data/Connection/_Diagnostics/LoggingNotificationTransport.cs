namespace EtAlii.Servus.Api
{
    using System;
    using EtAlii.xTechnology.Logging;

    public class LoggingNotificationTransport : INotificationTransport
    {
        private readonly INotificationTransport _transport;
        private readonly ILogger _logger;

        private string _address;

        public LoggingNotificationTransport(
            INotificationTransport transport, 
            ILogger logger)
        {
            _transport = transport;
            _logger = logger;
        }

        public void Open(string address)
        {
            _address = address;

            var message = String.Format("Opening notification transport (Address: {0})", _address);
            _logger.Info(message);
            var start = Environment.TickCount;

            _transport.Open(address);

            message = String.Format("Opened notification transport (Address: {0} Duration: {1}ms)", _address, Environment.TickCount - start);
            _logger.Info(message);

        }

        public void Close()
        {
            var message = String.Format("Closing notification transport (Address: {0})", _address);
            _logger.Info(message);
            var start = Environment.TickCount;

            _transport.Close();

            message = String.Format("Closed notification transport (Address: {0} Duration: {1}ms)", _address, Environment.TickCount - start);
            _logger.Info(message);
        }

        public void Register(INotificationClient client)
        {
            var message = String.Format("Registering client on notification transport (Address: {0})", _address);
            _logger.Info(message);
            var start = Environment.TickCount;

            _transport.Register(client);

            message = String.Format("Registered client on notification transport (Address: {0} Duration: {1}ms)", _address, Environment.TickCount - start);
            _logger.Info(message);
        }
    }
}
