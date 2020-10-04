namespace EtAlii.Ubigia.Api.Transport
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.xTechnology.MicroContainer;

    public abstract class SpaceTransportBase : ISpaceTransport
    {
        public bool IsConnected { get; private set; }

        public Uri Address { get; private set; }

        protected SpaceTransportBase(Uri address)
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

        IScaffolding[] ISpaceTransport.CreateScaffolding()
        {
            return CreateScaffoldingInternal();
        }
    }
}
