//namespace EtAlii.Ubigia.Infrastructure.Transport.Grpc
//[
//    using System
//    using System.Linq
//    using System.Security.Principal
//    using System.Threading
//    using EtAlii.Ubigia.Api.Transport
//    using EtAlii.Ubigia.Infrastructure.Functional
//    using Microsoft.ApplicationInsights.Grpc.Extensions
//    using Microsoft.Grpc.Http
//    using Microsoft.Grpc.Mvc
//    using Microsoft.Extensions.Primitives

//    internal class HttpContextResponseBuilder : IHttpContextResponseBuilder
//	[
//        private readonly IAccountRepository _accountRepository
//        private readonly IHttpContextAuthenticationIdentityProvider _authenticationIdentityProvider
//        private readonly IAuthenticationTokenConverter _authenticationTokenConverter

//        public HttpContextResponseBuilder(
//            IAccountRepository accountRepository,
//            IHttpContextAuthenticationIdentityProvider authenticationIdentityProvider,
//            IAuthenticationTokenConverter authenticationTokenConverter)
//        [
//            _accountRepository = accountRepository
//            _authenticationIdentityProvider = authenticationIdentityProvider
//            _authenticationTokenConverter = authenticationTokenConverter
//        }
		
//        public IActionResult Build(HttpContext context, Controller controller, string accountName)
//        [
//            IActionResult response
//            try
//            [
//                var success = context.Request.Headers.TryGetValue("Host-Identifier", out StringValues values)
//                if (success)
//                [
//                    var hostIdentifier = values.First()

//                    var authenticationToken = new AuthenticationToken
//                    [
//                        Name = accountName,
//                        Address = hostIdentifier,
//                        Salt = DateTime.UtcNow.ToBinary(),
//                    }

//                    var authenticationTokenAsBytes = _authenticationTokenConverter.ToBytes(authenticationToken)
//                    authenticationTokenAsBytes = Aes.Encrypt(authenticationTokenAsBytes)
//                    var authenticationTokenAsString = Convert.ToBase64String(authenticationTokenAsBytes)

//                    response = controller.Ok(authenticationTokenAsString)
//                }
//                else
//                [
//                    response = new StatusCodeResult(405); //HttpStatusCode.MethodNotAllowed
//                }
//            }
//            catch (Exception ex)
//            [
//                response = controller.BadRequest(ex.Message)
//                //response = actionContext.Request.CreateResponse<string>(HttpStatusCode.OK, "AllOk")
//            }
//            return response

//        }
//    }
//}