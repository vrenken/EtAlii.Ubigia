namespace EtAlii.Ubigia.Infrastructure.Transport.Owin.WebApi.Api.User
{
    using System.Net;
    using System.Web.Http.Controllers;
    using System.Web.Http.Filters;

    public class RequiresAuthenticationTokenAttribute : ActionFilterAttribute
    {
        private readonly string _requiredRole;
        private IAuthenticationTokenVerifier _verifier;

        public RequiresAuthenticationTokenAttribute(string requiredRole = null)
        {
            _requiredRole = requiredRole;
        }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            // Delayed dependency injection.
            // All other solutions are far slower.
            if (_verifier == null)
            {
                _verifier = (IAuthenticationTokenVerifier)actionContext.ControllerContext.Configuration.DependencyResolver.GetService(typeof(IAuthenticationTokenVerifier));
            }
            var status = _verifier.Verify(actionContext, _requiredRole);
            if (status == HttpStatusCode.OK || 
                status == HttpStatusCode.Unauthorized)
            {
                base.OnActionExecuting(actionContext);
            }
        }

    }
}