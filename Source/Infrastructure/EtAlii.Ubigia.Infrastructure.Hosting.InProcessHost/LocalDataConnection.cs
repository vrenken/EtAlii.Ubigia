namespace EtAlii.Ubigia.Infrastructure.Hosting.Local
{
    using EtAlii.Ubigia;

    public class LocalDataConnection : ILocalDataConnection
    {
        private readonly IDataConnection _connection;

        public LocalDataConnection(IDataConnection connection)
        {
            _connection = connection;
        }

        public IRootContext Roots { get { return _connection.Roots; } }
        public IEntryContext Entries { get { return _connection.Entries; } }
        public IContentContext Content { get { return _connection.Content; } }

        public bool IsConnected { get { return _connection.IsConnected; } }

        public void Open() { _connection.Open(); }
        public void Close() { _connection.Close(); }
    }
}
