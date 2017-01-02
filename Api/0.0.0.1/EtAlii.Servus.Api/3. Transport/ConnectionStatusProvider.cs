namespace EtAlii.Servus.Api.Transport
{
    using System;

    public class ConnectionStatusProvider : IConnectionStatusProvider
    {
        private IDataConnection _connection;

        public bool IsConnected { get { return _connection.Storage != null && _connection.Account != null && _connection.Space != null; } }
        public TimeSpan Duration { get { return TimeSpan.Zero; } }

        public void Initialize(IDataConnection connection)
        {
            _connection = connection;
        }
    }
}
