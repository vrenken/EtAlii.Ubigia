namespace EtAlii.Ubigia.Api.Management.WebApi
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Api.Transport.WebApi;

    internal abstract class WebApiClientBase
    {
        protected IWebApiStorageConnection Connection => _connection;
        private IWebApiStorageConnection _connection;

        protected WebApiClientBase()
        {
        }

        public async Task Connect(IStorageConnection storageConnection)
        {
            await Connect((IStorageConnection<IWebApiStorageTransport>)storageConnection);
        }

        public async Task Disconnect(IStorageConnection storageConnection)
        {
            await Connect((IStorageConnection<IWebApiStorageTransport>)storageConnection);
        }

        public virtual async Task Connect(IStorageConnection<IWebApiStorageTransport> storageConnection)
        {
            await Task.Run(() => _connection = (IWebApiStorageConnection)storageConnection);
        }

        public virtual async Task Disconnect(IStorageConnection<IWebApiStorageTransport> storageConnection)
        {
            await Task.Run(() => _connection = null);
        }
    }
}
