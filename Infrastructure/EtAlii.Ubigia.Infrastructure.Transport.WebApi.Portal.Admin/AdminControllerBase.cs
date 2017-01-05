namespace EtAlii.Ubigia.Infrastructure.Transport.WebApi.Portal.Admin
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using EtAlii.Ubigia.Api.Transport;

    [RequiresAuthenticationToken(Role.Admin)]
    public class AdminControllerBase : ApiController
    {
        protected AdminControllerBase()
        {
        }

        protected HttpResponseMessage CreateHttpResponse(HttpRequestMessage request, Func<HttpResponseMessage> function)
        {
            HttpResponseMessage response = null;

            try
            {
                response = function.Invoke();
            }
            catch (Exception ex)
            {
                //LogError(ex);
                response = request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

            return response;
        }
    }
}
