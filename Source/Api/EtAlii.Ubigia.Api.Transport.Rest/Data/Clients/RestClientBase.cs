namespace EtAlii.Ubigia.Api.Transport.Rest
{
    using System.Threading.Tasks;

    internal abstract class RestClientBase
    {
        protected IRestSpaceConnection Connection { get; private set; }

        public async Task Connect(ISpaceConnection spaceConnection)
        {
            await Connect((ISpaceConnection<IRestSpaceTransport>)spaceConnection).ConfigureAwait(false);
        }

        public virtual Task Connect(ISpaceConnection<IRestSpaceTransport> spaceConnection)
        {
            Connection = (IRestSpaceConnection)spaceConnection;
            return Task.CompletedTask;
        }

        public virtual Task Disconnect()
        {
            Connection = null;
            return Task.CompletedTask;
        }
    }
}
