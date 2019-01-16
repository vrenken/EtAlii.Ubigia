﻿//namespace EtAlii.Ubigia.Infrastructure.Transport.User.Api.Grpc
//{
//	using System;
//    using System.Collections.Generic;
//    using System.Linq;
//    using EtAlii.Ubigia.Api;
//    using EtAlii.Ubigia.Api.Transport;
//    using EtAlii.Ubigia.Infrastructure.Functional;
//	using Microsoft.Extensions.Primitives;

//	public class SpaceHub : HubBase
//    {
//		private readonly ISpaceRepository _items;
//		private readonly IAccountRepository _accountItems;
//		private readonly IAuthenticationTokenConverter _authenticationTokenConverter;

//		public SpaceHub(
//			ISpaceRepository items,
//			IAccountRepository accountItems,
//			ISimpleAuthenticationTokenVerifier authenticationTokenVerifier,
//			IAuthenticationTokenConverter authenticationTokenConverter)
//			: base(authenticationTokenVerifier)
//		{
//			_items = items;
//			_accountItems = accountItems;
//			_authenticationTokenConverter = authenticationTokenConverter;
//		}

//		public Space GetForAuthenticationToken(string spaceName)
//		{
//			Space response;
//			try
//			{
//				var httpContext = Context.Connection.GetHttpContext();
//				httpContext.Request.Headers.TryGetValue("Authentication-Token", out StringValues stringValues);
//				var authenticationTokenAsString = stringValues.Single();
//				var authenticationToken = _authenticationTokenConverter.FromString(authenticationTokenAsString);

//				var account = _accountItems.Get(authenticationToken.Name);

//				response = _items.Get(account.Id, spaceName);
//			}
//			catch (Exception e)
//			{
//				throw new InvalidOperationException("Unable to serve a Space GET client request", e);
//			}
//			return response;
//		}
//	}
//}