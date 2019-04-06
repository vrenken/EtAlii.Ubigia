namespace EtAlii.Ubigia.Api.Transport.Grpc
{
    using System.Threading.Tasks;

    public abstract class GrpcClientBase
    {
        protected ISpaceConnection<IGrpcSpaceTransport> Connection { get; private set; }

        public async Task Connect(ISpaceConnection spaceConnection)
        {
            await Connect((ISpaceConnection<IGrpcSpaceTransport>)spaceConnection);
        }

        public async Task Disconnect(ISpaceConnection spaceConnection)
        {
            await Disconnect((ISpaceConnection<IGrpcSpaceTransport>)spaceConnection);
        }

        public virtual Task Connect(ISpaceConnection<IGrpcSpaceTransport> spaceConnection)
        {
            Connection = spaceConnection;
            return Task.CompletedTask;
        }

        public virtual Task Disconnect(ISpaceConnection<IGrpcSpaceTransport> spaceConnection)
        {
            Connection = null;
            return Task.CompletedTask;
        }
    }
}
