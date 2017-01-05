namespace EtAlii.Ubigia.Infrastructure.Transport.Owin.WebApi
{
    using System.Web.Http;
    using EtAlii.Ubigia.Api.Transport.WebApi;

    [RequiresAuthentication]
    public class AuthenticateController : ApiController
    {
        [Route(RelativeUri.Authenticate), HttpGet]
        public void Get()
        {
            // All work is done in the AuthenticationFilter.
        }
    }
}
