namespace EtAlii.Ubigia.Api.Transport.SignalR
{
    using System.Threading.Tasks;

    public abstract class SignalRClientBase
    {
        protected ISpaceConnection<ISignalRSpaceTransport> Connection => _connection;
        private ISpaceConnection<ISignalRSpaceTransport> _connection;

        public async Task Connect(ISpaceConnection spaceConnection)
        {
            await Connect((ISpaceConnection<ISignalRSpaceTransport>)spaceConnection);
        }

        public async Task Disconnect(ISpaceConnection spaceConnection)
        {
            await Disconnect((ISpaceConnection<ISignalRSpaceTransport>)spaceConnection);
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
