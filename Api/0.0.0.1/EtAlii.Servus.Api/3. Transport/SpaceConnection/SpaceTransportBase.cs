namespace EtAlii.Servus.Api.Transport
{
    using System.Threading.Tasks;
    using EtAlii.xTechnology.MicroContainer;

    public abstract class SpaceTransportBase : ISpaceTransport
    {
        public bool IsConnected { get { return _isConnected; } }
        private bool _isConnected;

        public virtual void Initialize(ISpaceConnection spaceConnection, string address)
        {
        }

        public virtual async Task Start(ISpaceConnection spaceConnection, string address)
        {
            await Task.Run(() => { _isConnected = true; });
        }

        public virtual async Task Stop(ISpaceConnection spaceConnection)
        {
            await Task.Run(() => { _isConnected = false; });
        }

        protected abstract IScaffolding[] CreateScaffoldingInternal();

        IScaffolding[] ISpaceTransport.CreateScaffolding()
        {
            return CreateScaffoldingInternal();
        }
    }
}
