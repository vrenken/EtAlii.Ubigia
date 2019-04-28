namespace EtAlii.Ubigia.Api.Transport.Management.WebApi
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport.WebApi;

    public partial class WebApiAuthenticationManagementDataClient : IAuthenticationManagementDataClient
    {
        private IWebApiStorageConnection _connection;

        public Task Connect(IStorageConnection storageConnection)
        {
            return Connect((IWebApiStorageConnection) storageConnection);
        }

        public Task Disconnect(IStorageConnection storageConnection)
        {
            return Disconnect();
        }

        private Task Connect(IWebApiStorageConnection connection)
        {
            _connection = connection;
            return Task.CompletedTask;
        }

        private Task Disconnect()
        {
            _connection = null;
            return Task.CompletedTask;
        }
    }
}
