namespace EtAlii.Servus.Infrastructure.SignalR
{
    using System;
    using System.Net;
    using System.Security.Claims;
    using System.Web.Http.Controllers;
    using Microsoft.AspNet.SignalR;

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
    internal class RequiresAuthenticationAttribute : AuthorizeAttribute
    {
        private IAuthenticationVerifier _verifier;

        public RequiresAuthenticationAttribute(IAuthenticationVerifier verifier)
        {
            _verifier = verifier;
        }

        protected override bool UserAuthorized(System.Security.Principal.IPrincipal user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var principal = user as ClaimsPrincipal;

            //// Delayed dependency injection.
            //// All other solutions are far slower.
            //if (_verifier == null)
            //{
            //    _verifier = (IAuthenticationVerifier)this..ControllerContext.Configuration.DependencyResolver.GetService(typeof(IAuthenticationVerifier));
            //}

            if (principal != null)
            {
                Claim authenticated = principal.FindFirst(ClaimTypes.Authentication);
                if (authenticated != null && authenticated.Value == "true")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        ///// <summary>
        ///// Override to Web API filter method to handle Basic Auth check
        ///// </summary>
        ///// <param name="actionContext"></param>
        //public override void OnAuthorization(HttpActionContext actionContext)
        //{
        //    // Delayed dependency injection.
        //    // All other solutions are far slower.
        //    if (_verifier == null)
        //    {
        //        _verifier = (IAuthenticationVerifier)actionContext.ControllerContext.Configuration.DependencyResolver.GetService(typeof(IAuthenticationVerifier));
        //    }
        //    var status = _verifier.Verify(actionContext);
        //    if (status == HttpStatusCode.OK)
        //    {
        //        base.OnAuthorization(actionContext);
        //    }
        //}

    }
}