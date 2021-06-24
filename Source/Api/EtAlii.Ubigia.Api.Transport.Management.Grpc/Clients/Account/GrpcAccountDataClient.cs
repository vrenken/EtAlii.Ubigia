// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Management.Grpc
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport.Grpc;
    using EtAlii.Ubigia.Api.Transport.Management.Grpc.WireProtocol;
    using global::Grpc.Core;
    using Account = EtAlii.Ubigia.Account;
    using AdminAccountPostSingleRequest = EtAlii.Ubigia.Api.Transport.Management.Grpc.WireProtocol.AccountPostSingleRequest;
    using AdminAccountSingleRequest = EtAlii.Ubigia.Api.Transport.Management.Grpc.WireProtocol.AccountSingleRequest;
    using AdminAccountMultipleRequest = EtAlii.Ubigia.Api.Transport.Management.Grpc.WireProtocol.AccountMultipleRequest;

    public class GrpcAccountDataClient : IAccountDataClient<IGrpcStorageTransport>
    {
        private AccountGrpcService.AccountGrpcServiceClient _client;
        private IGrpcStorageTransport _transport;

        public async Task<Account> Add(string accountName, string accountPassword, AccountTemplate template)
        {
            try
            {
                var account = AccountExtension.ToWire(new Account
                {
                    Name = accountName,
                    Password = accountPassword,
                });

                var metadata = new Metadata { _transport.AuthenticationHeader };
                var request = new AdminAccountPostSingleRequest { Account = account, Template = template.Name };
                var call = _client.PostAsync(request, metadata);
                var response = await call.ResponseAsync.ConfigureAwait(false);

                return response.Account.ToLocal();
            }
            catch (RpcException e)
            {
                throw new InvalidInfrastructureOperationException($"{nameof(GrpcAccountDataClient)}.Add()", e);
            }
        }

        public async Task Remove(System.Guid accountId)
        {
            try
            {
                var metadata = new Metadata { _transport.AuthenticationHeader };
                var request = new AdminAccountSingleRequest { Id = GuidExtension.ToWire(accountId) };
                var call = _client.DeleteAsync(request, metadata);
                await call.ResponseAsync.ConfigureAwait(false);
            }
            catch (RpcException e)
            {
                throw new InvalidInfrastructureOperationException($"{nameof(GrpcAccountDataClient)}.Remove()", e);
            }
        }

        public async Task<Account> Change(System.Guid accountId, string accountName, string accountPassword)
        {
            try
            {
                var account = AccountExtension.ToWire(new Account
                {
                    Id = accountId,
                    Name = accountName,
                    Password = accountPassword,
                });

                var metadata = new Metadata { _transport.AuthenticationHeader };
                var request = new AdminAccountSingleRequest { Account = account };
                var call = _client.PutAsync(request, metadata);
                var response = await call.ResponseAsync.ConfigureAwait(false);

                return response.Account.ToLocal();
            }
            catch (RpcException e)
            {
                throw new InvalidInfrastructureOperationException($"{nameof(GrpcAccountDataClient)}.Change()", e);
            }
        }

        public async Task<Account> Change(Account account)
        {
            try
            {
                var metadata = new Metadata { _transport.AuthenticationHeader };
                var request = new AdminAccountSingleRequest { Account = AccountExtension.ToWire(account) };
                var call = _client.PutAsync(request, metadata);
                var response = await call.ResponseAsync.ConfigureAwait(false);

                return response.Account.ToLocal();
            }
            catch (RpcException e)
            {
                throw new InvalidInfrastructureOperationException($"{nameof(GrpcAccountDataClient)}.Change()", e);
            }
        }

        public async Task<Account> Get(string accountName)
        {
            try
            {
                var metadata = new Metadata { _transport.AuthenticationHeader };
                var request = new AdminAccountSingleRequest { Name = accountName };
                var call = _client.GetSingleAsync(request, metadata);
                var response = await call.ResponseAsync.ConfigureAwait(false);

                return response.Account.ToLocal();
            }
            catch (RpcException e)
            {
                throw new InvalidInfrastructureOperationException($"{nameof(GrpcAccountDataClient)}.Get()", e);
            }
        }

        public async Task<Account> Get(System.Guid accountId)
        {
            try
            {
                var metadata = new Metadata { _transport.AuthenticationHeader };
                var request = new AdminAccountSingleRequest { Id = GuidExtension.ToWire(accountId) };
                var call = _client.GetSingleAsync(request, metadata);
                var response = await call.ResponseAsync.ConfigureAwait(false);

                return response.Account.ToLocal();
            }
            catch (RpcException e)
            {
                throw new InvalidInfrastructureOperationException($"{nameof(GrpcAccountDataClient)}.Get()", e);
            }
        }

        public async IAsyncEnumerable<Account> GetAll()
        {
            var metadata = new Metadata { _transport.AuthenticationHeader };
            var request = new AdminAccountMultipleRequest();
            var call = _client.GetMultiple(request, metadata);

            // The structure below might seem weird.
            // But it is not possible to combine a try-catch with the yield needed
            // enumerating an IAsyncEnumerable.
            // The only way to solve this is using the enumerator.
            var enumerator = call.ResponseStream
                .ReadAllAsync()
                .GetAsyncEnumerator();
            var hasResult = true;
            while (hasResult)
            {
                AccountMultipleResponse item;
                try
                {
                    hasResult = await enumerator
                        .MoveNextAsync()
                        .ConfigureAwait(false);
                    item = hasResult ? enumerator.Current : null;
                }
                catch (RpcException e)
                {
                    throw new InvalidInfrastructureOperationException($"{nameof(GrpcAccountDataClient)}.GetAll()",e);
                }

                if (item != null)
                {
                    yield return item.Account.ToLocal();
                }
            }
        }

        public async Task Connect(IStorageConnection storageConnection)
        {
            await Connect((IStorageConnection<IGrpcStorageTransport>) storageConnection).ConfigureAwait(false);
        }

        public async Task Disconnect(IStorageConnection storageConnection)
        {
            await Disconnect((IStorageConnection<IGrpcStorageTransport>)storageConnection).ConfigureAwait(false);
        }

        public Task Connect(IStorageConnection<IGrpcStorageTransport> storageConnection)
        {
            _transport = storageConnection.Transport;
            _client = new AccountGrpcService.AccountGrpcServiceClient(_transport.CallInvoker);
            return Task.CompletedTask;
        }

        public Task Disconnect(IStorageConnection<IGrpcStorageTransport> storageConnection)
        {
            _transport = null;
            _client = null;
            return Task.CompletedTask;
        }
    }
}
