namespace EtAlii.Ubigia.Api.Transport.Management
{
	using System;
	using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.xTechnology.MicroContainer;

    public abstract class StorageTransportBase : IStorageTransport
    {
        public bool IsConnected { get; private set; }

        public Uri Address { get; }

        protected StorageTransportBase(Uri address)
        {
            Address = address;
        }
        
        public virtual async Task Start()
        {
            await Task.Run(() => { IsConnected = true; });

        }

        public virtual async Task Stop()
        {
            await Task.Run(() => { IsConnected = false; });
        }

        protected abstract IScaffolding[] CreateScaffoldingInternal();

        IScaffolding[] IStorageTransport.CreateScaffolding()
        {
            return CreateScaffoldingInternal();
        }
    }
}
