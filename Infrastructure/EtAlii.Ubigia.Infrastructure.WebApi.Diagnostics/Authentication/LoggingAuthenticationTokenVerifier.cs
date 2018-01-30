namespace EtAlii.Ubigia.Infrastructure.Transport.Owin.WebApi.Diagnostics
{
    using System.Net;
    using System.Web.Http.Controllers;

    //using EtAlii.xTechnology.Logging;

    internal class LoggingAuthenticationTokenVerifier : IAuthenticationTokenVerifier
    {
        private readonly IAuthenticationTokenVerifier _verifier;
        //private readonly ILogger _logger;

        public LoggingAuthenticationTokenVerifier(
            IAuthenticationTokenVerifier verifier
            //, ILogger logger
            )
        {
            _verifier = verifier;
            //_logger = logger;
        }

        public HttpStatusCode Verify(HttpActionContext actionContext, params string[] requiredRoles)
        {
            //var message = String.Format("Verifying authentication token");
            //_logger.Info(message);
            //var start = Environment.TickCount;

            var result = _verifier.Verify(actionContext, requiredRoles);

            //message = String.Format("Verified authentication token (Status: {0} Duration: {1}ms)", result, Environment.TickCount - start);
            //_logger.Info(message);

            return result;
        }
    }
}