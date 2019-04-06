namespace EtAlii.Ubigia.Api.Transport.WebApi
{
    using System.Threading.Tasks;

    internal abstract class WebApiClientBase
    {
        protected IWebApiSpaceConnection Connection { get; private set; }

        public async Task Connect(ISpaceConnection spaceConnection)
        {
            await Connect((ISpaceConnection<IWebApiSpaceTransport>)spaceConnection);
        }

        public async Task Disconnect(ISpaceConnection spaceConnection)
        {
            await Disconnect((ISpaceConnection<IWebApiSpaceTransport>)spaceConnection);
        }

        public virtual Task Connect(ISpaceConnection<IWebApiSpaceTransport> spaceConnection)
        {
            Connection = (IWebApiSpaceConnection)spaceConnection;
            return Task.CompletedTask;
        }

        public virtual Task Disconnect(ISpaceConnection<IWebApiSpaceTransport> spaceConnection)
        {
            Connection = null;
            return Task.CompletedTask;
        }
    }
}
