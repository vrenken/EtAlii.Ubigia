namespace EtAlii.Ubigia.Infrastructure.Transport.Admin.Api.Grpc
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport;
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
        public override Task<AccountSingleResponse> GetSingle(AccountSingleRequest request, ServerCallContext context)
        {
            EtAlii.Ubigia.Api.Account account;
            
            switch (request)
            {
                case var _ when request.Id != null: // Get Item by id
                    account = _items.Get(request.Id.ToLocal());
                    break;
                case var _ when request.Account != null: // Get Item by Instance
                    account = _items.Get(request.Account.Id.ToLocal());
                    break;
                case var _ when !String.IsNullOrWhiteSpace(request.Name): // Get Item by name
                    account = _items.Get(request.Name);
                    break;
                default:
                    throw new InvalidOperationException("Unable to serve a Account GET client request");                
            }

            var response = new AccountSingleResponse
            {
                Account = account.ToWire()
            };
            return Task.FromResult(response);
        }

        // Get all Items
        public override Task<AccountMultipleResponse> GetMultiple(AccountMultipleRequest request, ServerCallContext context)
        {
            var accounts = _items
                .GetAll()
                .ToWire();
            var response = new AccountMultipleResponse();
            response.Accounts.AddRange(accounts);

            return Task.FromResult(response);
        }

        public override async Task<AccountSingleResponse> Post(AccountPostSingleRequest request, ServerCallContext context)
        {
            var account = request.Account.ToLocal();
            var accountTemplate = request.Template;
            var template = AccountTemplate.All.Single(t => t.Name == accountTemplate);
            
            account = await _items.Add(account, template);

            var response = new AccountSingleResponse
            {
                Account = account.ToWire()
            };
            return response;
        }

        // Add item
        public override Task<AccountSingleResponse> Put(AccountSingleRequest request, ServerCallContext context)
        {
            var account = request.Account.ToLocal();
            account = _items.Update(account.Id, account);

            var response = new AccountSingleResponse
            {
                Account = account.ToWire()
            };
            return Task.FromResult(response);
        }

        // Update Item by id
        public override Task<AccountSingleResponse> Delete(AccountSingleRequest request, ServerCallContext context)
        {
            switch (request)
            {
                case var _ when request.Id != null: // Get Item by id
                    _items.Remove(request.Id.ToLocal());
                    break;
                case var _ when request.Account != null: // Get Item by id
                    _items.Remove(request.Account.Id.ToLocal());
                    break;
                default:
                    throw new InvalidOperationException("Unable to serve a Account DELETE client request");                
            }
            
            var response = new AccountSingleResponse
            {
                Account = request.Account
            };
            return Task.FromResult(response);
        }
    }
}
