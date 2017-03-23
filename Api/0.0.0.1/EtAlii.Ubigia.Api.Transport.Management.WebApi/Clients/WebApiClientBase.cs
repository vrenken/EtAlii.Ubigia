namespace EtAlii.Ubigia.Api.Transport.Management.WebApi
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Api.Transport.WebApi;

    internal abstract class WebApiClientBase
    {
        protected IWebApiStorageConnection Connection { get; private set; }

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
            await Task.Run(() => Connection = (IWebApiStorageConnection)storageConnection);
        }

        public virtual async Task Disconnect(IStorageConnection<IWebApiStorageTransport> storageConnection)
        {
            await Task.Run(() => Connection = null);
        }
    }
}
