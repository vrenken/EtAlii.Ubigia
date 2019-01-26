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
        
        public virtual async Task Start()
        {
            await Task.Run(() => { IsConnected = true; });
        }

        public virtual async Task Stop()
        {
            await Task.Run(() => { IsConnected = false; });
        }

        protected abstract IScaffolding[] CreateScaffoldingInternal();

        IScaffolding[] ISpaceTransport.CreateScaffolding()
        {
            return CreateScaffoldingInternal();
        }
    }
}
