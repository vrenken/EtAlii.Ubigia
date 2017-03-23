namespace EtAlii.Ubigia.Infrastructure.Transport
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport;

    internal abstract class SystemStorageClientBase
    {
        protected IStorageConnection Connection { get; private set; }

        public virtual async Task Connect(IStorageConnection storageConnection)
        {
            await Task.Run(() => Connection = storageConnection);
        }

        public virtual async Task Disconnect(IStorageConnection storageConnection)
        {
            await Task.Run(() => Connection = null);
        }
    }
}
