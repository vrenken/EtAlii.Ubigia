namespace EtAlii.Ubigia.Api.Transport.Management.WebApi
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport.WebApi;

    public partial class WebApiAuthenticationManagementDataClient : IAuthenticationManagementDataClient
    {
        //private IWebApiStorageConnection _connection

        public Task Connect(IStorageConnection storageConnection)
        {
            return Connect((IWebApiStorageConnection) storageConnection);
        }

        public Task Disconnect(IStorageConnection storageConnection)
        {
            return Disconnect();
        }

#pragma waring disable S1172 
        // ReSharper disable once UnusedParameter.Local
        private Task Connect(IWebApiStorageConnection connection)
#pragma waring restore S1172
        {
            //_connection = connection
            return Task.CompletedTask;
        }

        private Task Disconnect()
        {
            //_connection = null
            return Task.CompletedTask;
        }
    }
}
