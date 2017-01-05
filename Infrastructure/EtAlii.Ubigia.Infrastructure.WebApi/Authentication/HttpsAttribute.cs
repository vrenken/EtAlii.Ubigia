namespace EtAlii.Ubigia.Infrastructure.Transport.WebApi
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http.Controllers;
    using System.Web.Http.Filters;

    internal class HttpsAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (!String.Equals(actionContext.Request.RequestUri.Scheme, "https", StringComparison.OrdinalIgnoreCase))
            {
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent("HTTPS Required")
                };
                return;
            }
        }
    }
}