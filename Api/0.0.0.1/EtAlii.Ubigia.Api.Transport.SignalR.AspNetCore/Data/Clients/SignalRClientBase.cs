namespace EtAlii.Ubigia.Api.Transport.SignalR
{
    using System.Threading.Tasks;

    public abstract class SignalRClientBase
    {
        protected ISpaceConnection<ISignalRSpaceTransport> Connection { get; private set; }

        public async Task Connect(ISpaceConnection spaceConnection)
        {
            await Connect((ISpaceConnection<ISignalRSpaceTransport>)spaceConnection);
        }

        public async Task Disconnect(ISpaceConnection spaceConnection)
        {
            await Disconnect((ISpaceConnection<ISignalRSpaceTransport>)spaceConnection);
        }

        public virtual Task Connect(ISpaceConnection<ISignalRSpaceTransport> spaceConnection)
        {
            Connection = spaceConnection;
            return Task.CompletedTask;
        }

        public virtual Task Disconnect(ISpaceConnection<ISignalRSpaceTransport> spaceConnection)
        {
            Connection = null;            
            return Task.CompletedTask;
        }
    }
}
