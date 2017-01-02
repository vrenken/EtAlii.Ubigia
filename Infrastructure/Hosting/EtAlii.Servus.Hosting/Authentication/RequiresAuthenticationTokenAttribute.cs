namespace EtAlii.Servus.Infrastructure.Hosting
{
    using EtAlii.Servus.Infrastructure;
    using System.Net;
    using System.Web.Http.Controllers;
    using System.Web.Http.Filters;

    internal class RequiresAuthenticationTokenAttribute : ActionFilterAttribute
    {
        [InjectAttribute]
        public IAuthenticationTokenVerifier Verifier { get; set; }

        public RequiresAuthenticationTokenAttribute()
        {
        }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var status = Verifier.Verify(actionContext);
            if (status == HttpStatusCode.OK || 
                status == HttpStatusCode.Unauthorized)
            {
                base.OnActionExecuting(actionContext);
            }
        }

    }
}