//namespace EtAlii.Ubigia.Infrastructure.Transport.Grpc
//[
//    using System.Linq
//    using Microsoft.Extensions.Primitives

//    public static class AuthenticationTokenConverterExtension
//    [
//        public static AuthenticationToken FromHttpActionContext(this IAuthenticationTokenConverter converter, HttpContext actionContext)
//        [
//            actionContext.Request.Headers.TryGetValue("Authentication-Token", out StringValues values)
//            var authenticationTokenAsString = values.FirstOrDefault()
//            return authenticationTokenAsString != null ? converter.FromString(authenticationTokenAsString) : null
//        ]
//    ]
//]