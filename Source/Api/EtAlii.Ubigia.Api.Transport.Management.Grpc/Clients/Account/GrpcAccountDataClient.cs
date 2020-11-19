﻿namespace EtAlii.Ubigia.Api.Transport.Management.Grpc
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
    
                var request = new AdminAccountPostSingleRequest
                {
                    Account = account,
                    Template = template.Name
                };
                var call = _client.PostAsync(request, _transport.AuthenticationHeaders);
                var response = await call.ResponseAsync;
    
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
                var request = new AdminAccountSingleRequest
                {
                    Id = GuidExtension.ToWire(accountId)
                };
                var call = _client.DeleteAsync(request, _transport.AuthenticationHeaders);
                await call.ResponseAsync;
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
    
                var request = new AdminAccountSingleRequest
                {
                    Account = account,
                };
                var call = _client.PutAsync(request, _transport.AuthenticationHeaders);
                var response = await call.ResponseAsync;
    
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
                var request = new AdminAccountSingleRequest
                {
                    Account = AccountExtension.ToWire(account),
                };
                var call = _client.PutAsync(request, _transport.AuthenticationHeaders);
                var response = await call.ResponseAsync;
    
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
                var request = new AdminAccountSingleRequest
                {
                    Name = accountName,
                };
                var call = _client.GetSingleAsync(request, _transport.AuthenticationHeaders);
                var response = await call.ResponseAsync;
    
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
                var request = new AdminAccountSingleRequest
                {
                    Id = GuidExtension.ToWire(accountId)
                };
                var call = _client.GetSingleAsync(request, _transport.AuthenticationHeaders);
                var response = await call.ResponseAsync;
    
                return response.Account.ToLocal();
            }
            catch (RpcException e)
            {
                throw new InvalidInfrastructureOperationException($"{nameof(GrpcAccountDataClient)}.Get()", e);
            }
        }

        public async IAsyncEnumerable<Account> GetAll()
        {
            var request = new AdminAccountMultipleRequest();
            var call = _client.GetMultiple(request, _transport.AuthenticationHeaders);

            // The structure below might seem weird,
            // but it is not possible to combine a try-catch with the yield needed
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
            await Connect((IStorageConnection<IGrpcStorageTransport>) storageConnection);
        }

        public async Task Disconnect(IStorageConnection storageConnection)
        {
            await Disconnect((IStorageConnection<IGrpcStorageTransport>)storageConnection);
        }

        public Task Connect(IStorageConnection<IGrpcStorageTransport> storageConnection)
        {
            _transport = storageConnection.Transport;
            _client = new AccountGrpcService.AccountGrpcServiceClient(_transport.Channel);
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
