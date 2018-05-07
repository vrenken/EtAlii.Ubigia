namespace EtAlii.Ubigia.Api.Transport.Management.Grpc
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Api.Transport.Grpc;
    using EtAlii.Ubigia.Api.Transport.Management.Grpc.WireProtocol;

    public class GrpcAccountDataClient : IAccountDataClient<IGrpcStorageTransport>
    {
        //private HubConnection _connection;
        private AccountGrpcService.AccountGrpcServiceClient _client;

//        private readonly IHubProxyMethodInvoker _invoker;
//
//        public GrpcAccountDataClient(IHubProxyMethodInvoker invoker)
//        {
//            _invoker = invoker;
//        }

        public async Task<Api.Account> Add(string accountName, string accountPassword, AccountTemplate template)
        {
            var account = new Api.Account
            {
                Name = accountName,
                Password = accountPassword,
            };

            return await _invoker.Invoke<Api.Account>(_connection, GrpcHub.Account, "Post", account, template.Name);
        }

        public async Task Remove(System.Guid accountId)
        {
            await _invoker.Invoke(_connection, GrpcHub.Account, "Delete", accountId);
        }

        public async Task<Api.Account> Change(System.Guid accountId, string accountName, string accountPassword)
        {
            var account = new Api.Account
            {
                Id = accountId,
                Name = accountName,
                Password = accountPassword,
            };

            return await _invoker.Invoke<Api.Account>(_connection, GrpcHub.Account, "Put", accountId, account);
        }

        public async Task<Api.Account> Change(Api.Account account)
        {
            return await _invoker.Invoke<Api.Account>(_connection, GrpcHub.Account, "Put", account.Id, account);
        }

        public async Task<Api.Account> Get(string accountName)
        {
            return await _invoker.Invoke<Api.Account>(_connection, GrpcHub.Account, "GetByName", accountName);
        }

        public async Task<Api.Account> Get(System.Guid accountId)
        {
            return await _invoker.Invoke<Api.Account>(_connection, GrpcHub.Account, "Get", accountId);
        }

        public async Task<IEnumerable<Api.Account>> GetAll()
        {
            return await _invoker.Invoke<IEnumerable<Api.Account>>(_connection, GrpcHub.Account, "GetAll");
        }

        public async Task Connect(IStorageConnection storageConnection)
        {
            await Connect((IStorageConnection<IGrpcStorageTransport>) storageConnection);
        }

        public async Task Disconnect(IStorageConnection storageConnection)
        {
            await Disconnect((IStorageConnection<IGrpcStorageTransport>)storageConnection);
        }

        public async Task Connect(IStorageConnection<IGrpcStorageTransport> storageConnection)
        {
            _connection = new HubConnectionFactory().Create(storageConnection.Transport, new Uri(storageConnection.Storage.Address + GrpcHub.BasePath + "/" + GrpcHub.Account, UriKind.Absolute));
			await _connection.StartAsync();
        }

        public async Task Disconnect(IStorageConnection<IGrpcStorageTransport> storageConnection)
        {
            await _connection.DisposeAsync();
            _connection = null;
        }
    }
}
