namespace EtAlii.Ubigia.Api.Transport.Management.WebApi
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport.WebApi;

    public partial class WebApiAuthenticationManagementDataClient : IAuthenticationManagementDataClient
    {
        private IWebApiStorageConnection _connection;

        public async Task Connect(IStorageConnection storageConnection)
        {
            await Connect((IWebApiStorageConnection) storageConnection);
        }

        public async Task Disconnect(IStorageConnection storageConnection)
        {
            await Disconnect((IWebApiStorageConnection) storageConnection);
        }

        private async Task Connect(IWebApiStorageConnection connection)
        {
            await Task.Run(() => { _connection = connection; });
        }

        private async Task Disconnect(IWebApiStorageConnection connection)
        {
            await Task.Run(() => { _connection = null; });
        }
    }
}
