namespace EtAlii.Ubigia.Api.Transport.Management.WebApi
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport.WebApi;

    internal abstract class WebApiClientBase
    {
        protected IWebApiStorageConnection Connection { get; private set; }

        public async Task Connect(IStorageConnection storageConnection)
        {
            await Connect((IStorageConnection<IWebApiStorageTransport>)storageConnection);
        }

        public virtual Task Connect(IStorageConnection<IWebApiStorageTransport> storageConnection)
        {
            Connection = (IWebApiStorageConnection)storageConnection;
            return Task.CompletedTask;
        }

        public async Task Disconnect(IStorageConnection storageConnection)
        {
            await Connect((IStorageConnection<IWebApiStorageTransport>)storageConnection);
        }

        public virtual Task Disconnect(IStorageConnection<IWebApiStorageTransport> storageConnection)
        {
            Connection = null;
            return Task.CompletedTask;
        }
    }
}
