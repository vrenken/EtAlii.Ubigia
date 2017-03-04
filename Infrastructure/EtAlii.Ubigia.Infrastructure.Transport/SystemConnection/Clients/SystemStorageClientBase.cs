namespace EtAlii.Ubigia.Infrastructure.Transport
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport;

    internal abstract class SystemStorageClientBase
    {
        protected IStorageConnection Connection => _connection;
        private IStorageConnection _connection;

        public virtual async Task Connect(IStorageConnection storageConnection)
        {
            await Task.Run(() => _connection = storageConnection);
        }

        public virtual async Task Disconnect(IStorageConnection storageConnection)
        {
            await Task.Run(() => _connection = null);
        }
    }
}
