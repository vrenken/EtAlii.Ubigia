namespace EtAlii.Ubigia.Api.Transport.Management.Rest
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport.Rest;

    public partial class RestAuthenticationManagementDataClient : IAuthenticationManagementDataClient
    {
        //private IRestStorageConnection _connection

        public Task Connect(IStorageConnection storageConnection)
        {
            return Connect((IRestStorageConnection) storageConnection);
        }

        public Task Disconnect(IStorageConnection storageConnection)
        {
            return Disconnect();
        }

#pragma warning disable S1172
        // ReSharper disable once UnusedParameter.Local
        private Task Connect(IRestStorageConnection connection)
#pragma warning restore S1172
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
