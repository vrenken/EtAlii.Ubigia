// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Transport.Admin.Api.Grpc
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport.Management.Grpc;
    using EtAlii.Ubigia.Api.Transport.Management.Grpc.WireProtocol;
    using EtAlii.Ubigia.Infrastructure.Functional;
    using global::Grpc.Core;

    public class AdminAccountService : AccountGrpcService.AccountGrpcServiceBase, IAdminAccountService
    {
		private readonly IAccountRepository _items;

		public AdminAccountService(IAccountRepository items)
		{
			_items = items;
		}


        //public Account GetByName(string accountName)
        public override async Task<AccountSingleResponse> GetSingle(AccountSingleRequest request, ServerCallContext context)
        {
            EtAlii.Ubigia.Account account;

            switch (request)
            {
                case var _ when request.Id != null: // Get Item by id
                    account = await _items.Get(request.Id.ToLocal()).ConfigureAwait(false);
                    break;
                case var _ when request.Account != null: // Get Item by Instance
                    account = await _items.Get(request.Account.Id.ToLocal()).ConfigureAwait(false);
                    break;
                case var _ when !string.IsNullOrWhiteSpace(request.Name): // Get Item by name
                    account = await _items.Get(request.Name).ConfigureAwait(false);
                    break;
                default:
                    throw new InvalidOperationException("Unable to serve a Account GET client request");
            }

            var response = new AccountSingleResponse
            {
                Account = account.ToWire()
            };
            return response;
        }

        // Get all Items
        public override async Task GetMultiple(AccountMultipleRequest request, IServerStreamWriter<AccountMultipleResponse> responseStream, ServerCallContext context)
        {
            var items = _items
                .GetAll()
                .ConfigureAwait(false);
            await foreach (var item in items)
            {
                var response = new AccountMultipleResponse
                {
                    Account = item.ToWire()
                };
                await responseStream.WriteAsync(response).ConfigureAwait(false);
            }
        }

        public override async Task<AccountSingleResponse> Post(AccountPostSingleRequest request, ServerCallContext context)
        {
            var account = request.Account.ToLocal();
            var accountTemplate = request.Template;
            var template = AccountTemplate.All.Single(t => t.Name == accountTemplate);

            account = await _items.Add(account, template).ConfigureAwait(false);

            var response = new AccountSingleResponse
            {
                Account = account.ToWire()
            };
            return response;
        }

        // Add item
        public override async Task<AccountSingleResponse> Put(AccountSingleRequest request, ServerCallContext context)
        {
            var account = request.Account.ToLocal();
            account = await _items.Update(account.Id, account).ConfigureAwait(false);

            var response = new AccountSingleResponse
            {
                Account = account.ToWire()
            };
            return response;
        }

        // Update Item by id
        public override async Task<AccountSingleResponse> Delete(AccountSingleRequest request, ServerCallContext context)
        {
            switch (request)
            {
                case var _ when request.Id != null: // Get Item by id
                    await _items.Remove(request.Id.ToLocal()).ConfigureAwait(false);
                    break;
                case var _ when request.Account != null: // Get Item by id
                    await _items.Remove(request.Account.Id.ToLocal()).ConfigureAwait(false);
                    break;
                default:
                    throw new InvalidOperationException("Unable to serve a Account DELETE client request");
            }

            var response = new AccountSingleResponse
            {
                Account = request.Account
            };
            return response;
        }
    }
}
