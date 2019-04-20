//namespace EtAlii.Ubigia.Infrastructure.Transport.Grpc
//[
//    using System
//    using System.Linq
//    using EtAlii.Ubigia.Infrastructure.Functional
//    using Microsoft.Grpc.Http
//    using Microsoft.Grpc.Mvc

//    internal class HttpContextAuthenticationTokenVerifier : IHttpContextAuthenticationTokenVerifier
//    [
//        private readonly IAccountRepository _accountRepository
//        private readonly IAuthenticationTokenConverter _authenticationTokenConverter

//        public HttpContextAuthenticationTokenVerifier(
//            IAccountRepository accountRepository,
//            IAuthenticationTokenConverter authenticationTokenConverter)
//        [
//            _accountRepository = accountRepository
//            _authenticationTokenConverter = authenticationTokenConverter
//        ]
//        public IActionResult Verify(HttpContext context, Controller controller, params string[] requiredRoles)
//        [
//            IActionResult result = controller.Forbid()
//            var authenticationToken = _authenticationTokenConverter.FromHttpActionContext(context)
//            if [authenticationToken != null]
//            [
//                try
//                [
//                    var account = _accountRepository.Get(authenticationToken.Name)
//                    if [account != null]
//                    [
//	                    // Let's be a bit safe, if the requiredRole is not null we are going to check the roles collection for it.
//	                    if [requiredRoles.Any[]]
//	                    [
//		                    var hasOneRequiredRole = account.Roles.Any(role => requiredRoles.Any(requiredRole => requiredRole == role))
//		                    if [hasOneRequiredRole]
//		                    [
//			                    result = controller.Ok()
//		                    ]
//	                    ]
//                        else
//                        [
//                            result = controller.Ok()
//                        ]
//                    ]
//                ]
//                catch (Exception)
//                [
//                    result = controller.Forbid("Unauthorized account")
//                ]
//            ]
//            else
//            [
//                result = controller.BadRequest("Missing Authentication-Token")
//            ]
//            return result
//        ]
//    ]
//]