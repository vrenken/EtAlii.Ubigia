﻿namespace EtAlii.Ubigia.Infrastructure.Transport.Admin.Api.SignalR
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Infrastructure.Functional;
    using Microsoft.AspNetCore.SignalR;

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
				httpContext.Request.Headers.TryGetValue("Authentication-Token", out var stringValues);
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
		public async IAsyncEnumerable<Account> GetAll()
        {
            // The structure below might seem weird.
            // But it is not possible to combine a try-catch with the yield needed
            // enumerating an IAsyncEnumerable.
            // The only way to solve this is using the enumerator. 
            var enumerator = _items
                .GetAll()
                .GetAsyncEnumerator();
            var hasResult = true;
            while (hasResult)
            {
                Account item;
                try
                {
                    hasResult = await enumerator
                        .MoveNextAsync()
                        .ConfigureAwait(false);
                    item = hasResult ? enumerator.Current : null;
                }
                catch (Exception e)
                {
                    throw new InvalidOperationException("Unable to serve a Account GET client request", e);
                }
                if (item != null)
                {
                    yield return item;
                }
            }
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
        public async Task<Account> Post(Account item, string accountTemplate)
        {
            Account response;
            try
            {
                var template = AccountTemplate.All.Single(t => t.Name == accountTemplate);
                response = await _items.Add(item, template).ConfigureAwait(false);
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
