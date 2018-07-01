namespace EtAlii.Ubigia.Api.Transport.Management.Grpc
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport;
    using AdminAccountPostSingleRequest = EtAlii.Ubigia.Api.Transport.Management.Grpc.WireProtocol.AccountPostSingleRequest;
    using AdminAccountSingleRequest = EtAlii.Ubigia.Api.Transport.Management.Grpc.WireProtocol.AccountSingleRequest;
    using AdminAccountMultipleRequest = EtAlii.Ubigia.Api.Transport.Management.Grpc.WireProtocol.AccountMultipleRequest;
    using IGrpcStorageTransport = EtAlii.Ubigia.Api.Transport.Grpc.IGrpcStorageTransport;
    using EtAlii.Ubigia.Api.Transport.Management.Grpc.WireProtocol;

    public class GrpcAccountDataClient : IAccountDataClient<IGrpcStorageTransport>
    {
        private AccountGrpcService.AccountGrpcServiceClient _client;

        public async Task<Api.Account> Add(string accountName, string accountPassword, AccountTemplate template)
        {
            var account = new Api.Account
            {
                Name = accountName,
                Password = accountPassword,
            }.ToWire();

            var request = new AdminAccountPostSingleRequest
            {
                Account = account,
                Template = template.Name
            };
            var call = _client.PostAsync(request);
            var response = await call.ResponseAsync
                .ConfigureAwait(false);

            return response.Account.ToLocal();
            //return await _invoker.Invoke<Api.Account>(_connection, GrpcHub.Account, "Post", account, template.Name);
        }

        public async Task Remove(System.Guid accountId)
        {
            var request = new AdminAccountSingleRequest
            {
                Id = accountId.ToWire()
            };
            var call = _client.DeleteAsync(request);
            var response = await call.ResponseAsync
                .ConfigureAwait(false);
            //await _invoker.Invoke(_connection, GrpcHub.Account, "Delete", accountId);
        }

        public async Task<Api.Account> Change(System.Guid accountId, string accountName, string accountPassword)
        {
            var account = new Api.Account
            {
                Id = accountId,
                Name = accountName,
                Password = accountPassword,
            }.ToWire();

            var request = new AdminAccountSingleRequest
            {
                Account = account,
            };
            var call = _client.PutAsync(request);
            var response = await call.ResponseAsync
                .ConfigureAwait(false);

            return response.Account.ToLocal();
            //return await _invoker.Invoke<Api.Account>(_connection, GrpcHub.Account, "Put", accountId, account);
        }

        public async Task<Api.Account> Change(Api.Account account)
        {
            var request = new AdminAccountSingleRequest
            {
                Account = account.ToWire(),
            };
            var call = _client.PutAsync(request);
            var response = await call.ResponseAsync
                .ConfigureAwait(false);

            return response.Account.ToLocal();
            //return await _invoker.Invoke<Api.Account>(_connection, GrpcHub.Account, "Put", account.Id, account);
        }

        public async Task<Api.Account> Get(string accountName)
        {
            var request = new AdminAccountSingleRequest
            {
                Name = accountName,
            };
            var call = _client.GetSingleAsync(request);
            var response = await call.ResponseAsync
                .ConfigureAwait(false);

            return response.Account.ToLocal();
            //return await _invoker.Invoke<Api.Account>(_connection, GrpcHub.Account, "GetByName", accountName);
        }

        public async Task<Api.Account> Get(System.Guid accountId)
        {
            var request = new AdminAccountSingleRequest
            {
                Id = accountId.ToWire()
            };
            var call = _client.GetSingleAsync(request);
            var response = await call.ResponseAsync
                .ConfigureAwait(false);

            return response.Account.ToLocal();
            //return await _invoker.Invoke<Api.Account>(_connection, GrpcHub.Account, "Get", accountId);
        }

        public async Task<IEnumerable<Api.Account>> GetAll()
        {
            var request = new AdminAccountMultipleRequest
            {
            };
            var call = _client.GetMultipleAsync(request);
            var response = await call.ResponseAsync
                .ConfigureAwait(false);

            return response.Accounts.ToLocal();
            //return await _invoker.Invoke<IEnumerable<Api.Account>>(_connection, GrpcHub.Account, "GetAll");
        }

        public async Task Connect(IStorageConnection storageConnection)
        {
            await Connect((IStorageConnection<IGrpcStorageTransport>) storageConnection);
        }

        public async Task Disconnect(IStorageConnection storageConnection)
        {
            await Disconnect((IStorageConnection<IGrpcStorageTransport>)storageConnection);
        }

        public Task Connect(IStorageConnection<IGrpcStorageTransport> storageConnection)
        {
            var channel = storageConnection.Transport.Channel;
            _client = new AccountGrpcService.AccountGrpcServiceClient(channel);
            return Task.CompletedTask;
        }

        public Task Disconnect(IStorageConnection<IGrpcStorageTransport> storageConnection)
        {
            _client = null;
            return Task.CompletedTask;
        }
    }
}
