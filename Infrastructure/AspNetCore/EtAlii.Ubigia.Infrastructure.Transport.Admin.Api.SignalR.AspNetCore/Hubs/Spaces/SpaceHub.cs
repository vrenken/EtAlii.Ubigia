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

	public class SpaceHub : HubBase
    {
		private readonly ISpaceRepository _items;
		private readonly IAccountRepository _accountItems;
		private readonly IAuthenticationTokenConverter _authenticationTokenConverter;

		public SpaceHub(
			ISpaceRepository items,
			IAccountRepository accountItems,
			ISimpleAuthenticationTokenVerifier authenticationTokenVerifier,
			IAuthenticationTokenConverter authenticationTokenConverter)
			: base(authenticationTokenVerifier)
		{
			_items = items;
			_accountItems = accountItems;
			_authenticationTokenConverter = authenticationTokenConverter;
		}

		// Get all spaces for the specified accountid
		public IEnumerable<Space> GetAllForAccount(Guid accountId)
        {
            IEnumerable<Space> response;
            try
            {
                response = _items.GetAll(accountId);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("Unable to serve a Space GET client request", e);
            }
            return response;
        }

		public Space GetForAccount(Guid accountId, string spaceName)
		{
			Space response;
			try
			{
				response = _items.Get(accountId, spaceName);
			}
			catch (Exception e)
			{
				throw new InvalidOperationException("Unable to serve a Space GET client request", e);
			}
			return response;
		}

		public Space GetForAuthenticationToken(string spaceName)
		{
			Space response;
			try
			{
				var httpContext = Context.GetHttpContext();
				httpContext.Request.Headers.TryGetValue("Authentication-Token", out StringValues stringValues);
				var authenticationTokenAsString = stringValues.Single();
				var authenticationToken = _authenticationTokenConverter.FromString(authenticationTokenAsString);

				var account = _accountItems.Get(authenticationToken.Name);

				response = _items.Get(account.Id, spaceName);
			}
			catch (Exception e)
			{
				throw new InvalidOperationException("Unable to serve a Space GET client request", e);
			}
			return response;
		}

		// Get all Items
		public IEnumerable<Space> GetAll()
        {
            IEnumerable<Space> response;
            try
            {
                response = _items.GetAll();
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("Unable to serve a Space GET client request", e);
            }
            return response;
        }

        // Get Item by id
        public Space Get(Guid spaceId)
        {
            Space response;
            try
            {
                response = _items.Get(spaceId);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("Unable to serve a Space GET client request", e);
            }
            return response;
        }

        // Add item
        public Space Post(Space item, string spaceTemplate)
        {
            Space response;
            try
            {
                var template = SpaceTemplate.All.Single(t => t.Name == spaceTemplate);
                response = _items.Add(item, template);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("Unable to serve a Space POST client request", e);
            }
            return response;
        }

        // Update Item by id
        public Space Put(Guid spaceId, Space space)
        {
            Space response;
            try
            {
                response = _items.Update(spaceId, space);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("Unable to serve a Space PUT client request", e);
            }
            return response;
        }

        // Delete Item by id
        public void Delete(Guid spaceId)
        {
            try
            {
                _items.Remove(spaceId);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("Unable to serve a Space DELETE client request", e);
            }
        }
    }
}
