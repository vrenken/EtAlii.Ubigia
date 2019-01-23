namespace EtAlii.Ubigia.Infrastructure.Transport.AspNetCore
{
    using System;
    using System.Linq;
    using System.Net;
    using EtAlii.Ubigia.Infrastructure.Functional;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Primitives;

    internal class HttpContextResponseBuilder : IHttpContextResponseBuilder
	{
        private readonly IAccountRepository _accountRepository;
        private readonly IHttpContextAuthenticationIdentityProvider _authenticationIdentityProvider;
        private readonly IAuthenticationTokenConverter _authenticationTokenConverter;

        public HttpContextResponseBuilder(
            IAccountRepository accountRepository,
            IHttpContextAuthenticationIdentityProvider authenticationIdentityProvider,
            IAuthenticationTokenConverter authenticationTokenConverter)
        {
            _accountRepository = accountRepository;
            _authenticationIdentityProvider = authenticationIdentityProvider;
            _authenticationTokenConverter = authenticationTokenConverter;
        }
		
        public IActionResult Build(HttpContext context, Controller controller, string accountName)
        {
            IActionResult response;
            try
            {
                var success = context.Request.Headers.TryGetValue("Host-Identifier", out StringValues values);
                if (success)
                {
                    var hostIdentifier = values.First();

                    var authenticationToken = new AuthenticationToken
                    {
                        Name = accountName,
                        Address = hostIdentifier,
                        Salt = DateTime.UtcNow.ToBinary(),
                    };

                    var authenticationTokenAsBytes = _authenticationTokenConverter.ToBytes(authenticationToken);
                    authenticationTokenAsBytes = Aes.Encrypt(authenticationTokenAsBytes);
                    var authenticationTokenAsString = Convert.ToBase64String(authenticationTokenAsBytes);

                    response = controller.Ok(authenticationTokenAsString);
                }
                else
                {
                    response = new StatusCodeResult((int)HttpStatusCode.MethodNotAllowed); //405
                }
            }
            catch (Exception ex)
            {
                response = controller.BadRequest(ex.Message);
            }
            return response;

        }
    }
}