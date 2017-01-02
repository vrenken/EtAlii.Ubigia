namespace EtAlii.Servus.Api.Transport
{
    using System;

    public class DataConnectionContext : IDataConnectionContext
    {
        public IDataConnection DataConnection { get { return _dataConnection; } }
        private IDataConnection _dataConnection;

        public IAddressFactory AddressFactory { get { return _storageConnection.AddressFactory; } }
        public IInfrastructureClient Client { get { return _storageConnection.Client; } }

        private readonly IStorageConnection _storageConnection;

        public DataConnectionContext(IStorageConnection storageConnection)
        {
            _storageConnection = storageConnection;
        }

        public void Initialize(IDataConnection dataConnection)
        {
            if (_dataConnection == null)
            {
                _dataConnection = dataConnection;
            }
            else
            {
                throw new NotSupportedException("The DataConnectionContext can only be initialized once");
            }
        }
    }
}
