namespace EtAlii.Servus.Api.Transport.SignalR
{
    using System.Threading.Tasks;

    public abstract class SignalRClientBase
    {
        protected ISpaceConnection<ISignalRSpaceTransport> Connection { get { return _connection; } }
        private ISpaceConnection<ISignalRSpaceTransport> _connection;

        protected SignalRClientBase()
        {
        }
        public async Task Connect(ISpaceConnection spaceConnection)
        {
            await this.Connect((ISpaceConnection<ISignalRSpaceTransport>)spaceConnection);
        }

        public async Task Disconnect(ISpaceConnection spaceConnection)
        {
            await this.Disconnect((ISpaceConnection<ISignalRSpaceTransport>)spaceConnection);
        }

        public virtual async Task Connect(ISpaceConnection<ISignalRSpaceTransport> spaceConnection)
        {
            await Task.Run(() => _connection = spaceConnection);
        }

        public virtual async Task Disconnect(ISpaceConnection<ISignalRSpaceTransport> spaceConnection)
        {
            await Task.Run(() => _connection = null);
        }
    }
}
