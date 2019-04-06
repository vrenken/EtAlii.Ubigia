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
        
        public virtual Task Start()
        {
            IsConnected = true;
            return Task.CompletedTask;
        }

        public virtual Task Stop()
        {
            IsConnected = false; 
            return Task.CompletedTask;
        }

        protected abstract IScaffolding[] CreateScaffoldingInternal();

        IScaffolding[] IStorageTransport.CreateScaffolding()
        {
            return CreateScaffoldingInternal();
        }
    }
}
