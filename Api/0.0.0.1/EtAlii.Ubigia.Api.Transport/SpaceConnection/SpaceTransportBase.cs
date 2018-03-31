namespace EtAlii.Ubigia.Api.Transport
{
	using System;
	using System.Threading.Tasks;
    using EtAlii.xTechnology.MicroContainer;

    public abstract class SpaceTransportBase : ISpaceTransport
    {
        public bool IsConnected { get; private set; }

        public virtual void Initialize(ISpaceConnection spaceConnection, Uri address)
        {
        }

        public virtual async Task Start(ISpaceConnection spaceConnection, Uri address)
        {
            await Task.Run(() => { IsConnected = true; });
        }

        public virtual async Task Stop(ISpaceConnection spaceConnection)
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
