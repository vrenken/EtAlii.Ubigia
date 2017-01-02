namespace EtAlii.Servus.Api.Management.WebApi
{
    using System.Threading.Tasks;
    using EtAlii.Servus.Api.Transport;
    using EtAlii.Servus.Api.Transport.WebApi;

    internal abstract class WebApiClientBase
    {
        protected IWebApiStorageConnection Connection { get { return _connection; } }
        private IWebApiStorageConnection _connection;

        protected WebApiClientBase()
        {
        }

        public async Task Connect(IStorageConnection storageConnection)
        {
            await this.Connect((IStorageConnection<IWebApiStorageTransport>)storageConnection);
        }

        public async Task Disconnect(IStorageConnection storageConnection)
        {
            await this.Connect((IStorageConnection<IWebApiStorageTransport>)storageConnection);
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
