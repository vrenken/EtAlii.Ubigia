namespace EtAlii.Ubigia.Api.Transport.Management.WebApi
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport.WebApi;

    public partial class WebApiAuthenticationManagementDataClient : IAuthenticationManagementDataClient
    {
        private IWebApiStorageConnection _connection;

        public async Task Connect(IStorageConnection connection)
        {
            await Connect((IWebApiStorageConnection) connection);
        }

        public async Task Disconnect(IStorageConnection connection)
        {
            await Disconnect((IWebApiStorageConnection) connection);
        }

        private Task Connect(IWebApiStorageConnection connection)
        {
            _connection = connection;
            return Task.CompletedTask;
        }

        private Task Disconnect(IWebApiStorageConnection connection)
        {
            _connection = null;
            return Task.CompletedTask;
        }
    }
}
