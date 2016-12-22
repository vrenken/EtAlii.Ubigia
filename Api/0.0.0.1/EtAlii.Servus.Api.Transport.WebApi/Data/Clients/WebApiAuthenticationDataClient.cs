namespace EtAlii.Servus.Api.Transport.WebApi
{
    using System.Threading.Tasks;

    public partial class WebApiAuthenticationDataClient : IAuthenticationDataClient
    {
        private IWebApiSpaceConnection _connection;

        public WebApiAuthenticationDataClient()
        {
        }

        public async Task Connect(ISpaceConnection spaceConnection)
        {
            await this.Connect((IWebApiSpaceConnection) spaceConnection);
        }

        public async Task Disconnect(ISpaceConnection spaceConnection)
        {
            await this.Disconnect((IWebApiSpaceConnection) spaceConnection);
        }

        private async Task Connect(IWebApiSpaceConnection connection)
        {
            await Task.Run(() => { _connection = connection; });
        }

        private async Task Disconnect(IWebApiSpaceConnection connection)
        {
            await Task.Run(() => { _connection = null; });
        }
    }
}
