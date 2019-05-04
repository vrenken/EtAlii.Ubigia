//namespace EtAlii.Ubigia.Infrastructure.Transport.Owin.Tests
//[
//    using System
//    using System.Collections.Generic
//    using System.Linq
//    using System.Web.Http.Controllers
//    using EtAlii.Ubigia.Infrastructure.Transport
//    using EtAlii.Ubigia.Infrastructure.Transport.Owin.WebApi

//    internal class TestAuthenticationIdentityProvider : IAuthenticationIdentityProvider
//    [
//        public AuthenticationIdentity Get(HttpActionContext actionContext)
//        [
//            var userName = string.Empty
//            var password = string.Empty
//            IEnumerable<string> values
//            if [actionContext.Request.Headers.TryGetValues["Test-UserName", out values]]
//            [
//                userName = values.SingleOrDefault()
//            ]
//            if [actionContext.Request.Headers.TryGetValues["Test-Password", out values]]
//            [
//                password = values.SingleOrDefault()
//            ]
//            var hasCredentials = !string.IsNullOrWhiteSpace(password) && !string.IsNullOrWhiteSpace(userName)
//            return hasCredentials ? new AuthenticationIdentity(userName, password) : null
//        ]
//    ]
//]