namespace EtAlii.Servus.Infrastructure.Hosting
{
    using System.Web.Http;

    [RequiresAuthentication]
    public class AuthenticateController : ApiController
    {
        public void Get()
        {
            // All work is done in the AuthenticationFilter.
        }
    }
}
