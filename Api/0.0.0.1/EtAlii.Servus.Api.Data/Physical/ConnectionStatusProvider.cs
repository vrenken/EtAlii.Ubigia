namespace EtAlii.Servus.Api.Data
{
    public class ConnectionStatusProvider : IConnectionStatusProvider
    {
        public IDataConnection Connection { get; set; }

        public bool IsConnected { get { return GetIsConnected(); } }

        public bool GetIsConnected()
        {
            return Connection.Storage != null && Connection.Account != null && Connection.Space != null;
        }
    }
}
