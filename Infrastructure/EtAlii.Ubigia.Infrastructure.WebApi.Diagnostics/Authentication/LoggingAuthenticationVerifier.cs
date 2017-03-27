﻿namespace EtAlii.Ubigia.Infrastructure.Transport.Owin.WebApi.Diagnostics
{
    using System.Net;
    using System.Web.Http.Controllers;

    //using EtAlii.xTechnology.Logging;

    internal class LoggingAuthenticationVerifier : IAuthenticationVerifier
    {
        private readonly IAuthenticationVerifier _verifier;

        public LoggingAuthenticationVerifier(
            IAuthenticationVerifier verifier
            //,ILogger logger
            )
        {
            _verifier = verifier;
            //_logger = logger;
        }

        public HttpStatusCode Verify(HttpActionContext actionContext)
        {
            //var message = String.Format("Verifying authentication");
            //_logger.Info(message);
            //var start = Environment.TickCount;

            var result = _verifier.Verify(actionContext);

            //message = String.Format("Verified authentication (Status: {0} Duration: {1}ms)", result, Environment.TickCount - start);
            //_logger.Info(message);

            return result;
        }
    }
}