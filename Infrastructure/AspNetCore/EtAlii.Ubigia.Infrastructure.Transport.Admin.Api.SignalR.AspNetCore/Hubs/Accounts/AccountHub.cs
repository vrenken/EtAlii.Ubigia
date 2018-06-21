namespace EtAlii.Ubigia.Infrastructure.Transport.Admin.Api.SignalR.AspNetCore
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Infrastructure.Functional;
	using Microsoft.AspNetCore.SignalR;
	using Microsoft.Extensions.Primitives;

	public class AccountHub : HubBase
    {
		private readonly IAccountRepository _items;
		private readonly IAuthenticationTokenConverter _authenticationTokenConverter;

		public AccountHub(
			IAccountRepository items,
			ISimpleAuthenticationTokenVerifier authenticationTokenVerifier,
			IAuthenticationTokenConverter authenticationTokenConverter)
			: base(authenticationTokenVerifier)
		{
			_items = items;
			_authenticationTokenConverter = authenticationTokenConverter;
		}

		public Account GetByName(string accountName)
        {
            Account response;
            try
            {
                response = _items.Get(accountName);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("Unable to serve a Account GET client request", e);
            }
            return response;
        }

		public Account GetForAuthenticationToken()
		{
			Account response;
			try
			{
				var httpContext = Context.GetHttpContext();
				httpContext.Request.Headers.TryGetValue("Authentication-Token", out StringValues stringValues);
				var authenticationTokenAsString = stringValues.Single();
				var authenticationToken = _authenticationTokenConverter.FromString(authenticationTokenAsString);

				response = _items.Get(authenticationToken.Name);
			}
			catch (Exception e)
			{
				throw new InvalidOperationException("Unable to serve a Account GET client request", e);
			}
			return response;
		}


		// Get all Items
		public IEnumerable<Account> GetAll()
        {
            IEnumerable<Account> response;
            try
            {
                response = _items.GetAll();
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("Unable to serve a Account GET client request", e);
            }
            return response;
        }

        // Get Item by id
        public Account Get(Guid accountId)
        {
            Account response;
            try
            {
                response = _items.Get(accountId);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("Unable to serve a Account GET client request", e);
            }
            return response;
        }

        // Add item
        public Account Post(Account item, string accountTemplate)
        {
            Account response;
            try
            {
                var template = AccountTemplate.All.Single(t => t.Name == accountTemplate);
                response = _items.Add(item, template);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("Unable to serve a Account POST client request", e);
            }
            return response;
        }

        // Update Item by id
        public Account Put(Guid accountId, Account account)
        {
            Account response;
            try
            {
                response = _items.Update(accountId, account);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("Unable to serve a Account PUT client request", e);
            }
            return response;
        }

        // Delete Item by id
        public void Delete(Guid accountId)
        {
            try
            {
                _items.Remove(accountId);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("Unable to serve a Account DELETE client request", e);
            }
        }
    }
}
