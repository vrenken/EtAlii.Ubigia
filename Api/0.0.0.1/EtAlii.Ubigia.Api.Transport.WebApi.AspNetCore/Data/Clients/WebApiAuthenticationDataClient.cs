namespace EtAlii.Ubigia.Api.Transport.WebApi
{
    using System.Threading.Tasks;

    public partial class WebApiAuthenticationDataClient : IAuthenticationDataClient
    {
        private IWebApiSpaceConnection _connection;

        public async Task Connect(ISpaceConnection spaceConnection)
        {
            await Connect((IWebApiSpaceConnection) spaceConnection);
        }

        public async Task Disconnect(ISpaceConnection spaceConnection)
        {
            await Disconnect((IWebApiSpaceConnection) spaceConnection);
        }

        private Task Connect(IWebApiSpaceConnection connection)
        {
            _connection = connection;
            return Task.CompletedTask;
        }

        private Task Disconnect(IWebApiSpaceConnection connection)
        {
            _connection = null;
            return Task.CompletedTask;
        }
    }
}
