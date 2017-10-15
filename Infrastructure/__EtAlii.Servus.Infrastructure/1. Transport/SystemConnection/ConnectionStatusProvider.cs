namespace EtAlii.Servus.Infrastructure
{
    // TODO: We have 3 ConnectionStatusProvider, please merge.
    public class ConnectionStatusProvider : IConnectionStatusProvider
    {
        private ISystemConnection _connection;

        public bool IsConnected { get { return _connection.Storage != null; } }

        public void Initialize(ISystemConnection connection)
        {
            _connection = connection;
        }
    }
}
