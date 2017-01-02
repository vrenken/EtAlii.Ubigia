namespace EtAlii.Ubigia.Api.Management
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.xTechnology.MicroContainer;

    public abstract class StorageTransportBase : IStorageTransport
    {
        public bool IsConnected { get { return _isConnected; } }
        private bool _isConnected;

        public virtual void Initialize(IStorageConnection storageConnection, string address)
        {
        }

        public virtual async Task Start(IStorageConnection storageConnection, string address)
        {
            await Task.Run(() => { _isConnected = true; });

        }

        public virtual async Task Stop(IStorageConnection storageConnection)
        {
            await Task.Run(() => { _isConnected = false; });
        }

        protected abstract IScaffolding[] CreateScaffoldingInternal();

        IScaffolding[] IStorageTransport.CreateScaffolding()
        {
            return CreateScaffoldingInternal();
        }
    }
}
