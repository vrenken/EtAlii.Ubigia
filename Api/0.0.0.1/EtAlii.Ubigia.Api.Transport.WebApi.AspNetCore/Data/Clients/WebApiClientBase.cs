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

        public virtual async Task Connect(ISpaceConnection<IWebApiSpaceTransport> spaceConnection)
        {
            await Task.Run(() => Connection = (IWebApiSpaceConnection)spaceConnection);
        }

        public virtual async Task Disconnect(ISpaceConnection<IWebApiSpaceTransport> spaceConnection)
        {
            await Task.Run(() => Connection = null);
        }
    }
}
