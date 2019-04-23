//namespace EtAlii.Ubigia.Infrastructure.Transport.User.Api.Grpc
//[
//	using System
//    using System.Collections.Generic
//    using System.Linq
//    using EtAlii.Ubigia.Api
//    using EtAlii.Ubigia.Api.Transport
//    using EtAlii.Ubigia.Infrastructure.Functional
//	using Microsoft.Extensions.Primitives

//	public class AccountHub : HubBase
//    [
//        private readonly IAccountRepository _items
//		private readonly IAuthenticationTokenConverter _authenticationTokenConverter

//		public AccountHub(
//            IAccountRepository items,
//            ISimpleAuthenticationTokenVerifier authenticationTokenVerifier,
//			IAuthenticationTokenConverter authenticationTokenConverter)
//            : base(authenticationTokenVerifier)
//        [
//            _items = items
//			_authenticationTokenConverter = authenticationTokenConverter
//		]
//		public Account GetForAuthenticationToken()
//		[
//			Account response
//			try
//			[
//				var httpContext = Context.Connection.GetHttpContext()
//				httpContext.Request.Headers.TryGetValue("Authentication-Token", out StringValues stringValues)
//				var authenticationTokenAsString = stringValues.Single()
//				var authenticationToken = _authenticationTokenConverter.FromString(authenticationTokenAsString)

//				response = _items.Get(authenticationToken.Name)
//			]
//			catch [Exception ex]
//			[
//				throw new InvalidOperationException("Unable to serve a Account GET client request", e)
//			]
//			return response
//		]
//    ]
//]