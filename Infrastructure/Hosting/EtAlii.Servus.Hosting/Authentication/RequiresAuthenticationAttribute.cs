namespace EtAlii.Servus.Infrastructure.Hosting
{
    using EtAlii.Servus.Infrastructure;
    using System;
    using System.Net;
    using System.Web.Http.Controllers;
    using System.Web.Http.Filters;

    /// <summary>
    /// Generic Basic Authentication filter that checks for basic authentication
    /// headers and challenges for authentication if no authentication is provided
    /// Sets the Thread Principle with a GenericAuthenticationPrincipal.
    /// 
    /// You can override the OnAuthorize method for custom auth logic that
    /// might be application specific.    
    /// </summary>
    /// <remarks>Always remember that Basic Authentication passes accountname and passwords
    /// from client to server in plain text, so make sure SSL is used with basic auth
    /// to encode the Authorization header on all requests (not just the login).
    /// </remarks>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    internal class RequiresAuthenticationAttribute : AuthorizationFilterAttribute
    {
        [InjectAttribute]
        public IAuthenticationVerifier Verifier { get; set; }

        public RequiresAuthenticationAttribute()
        { 
        }

        /// <summary>
        /// Override to Web API filter method to handle Basic Auth check
        /// </summary>
        /// <param name="actionContext"></param>
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            var status = Verifier.Verify(actionContext);
            if (status == HttpStatusCode.OK)
            {
                base.OnAuthorization(actionContext);
            }
        }

    }
}