//namespace EtAlii.Ubigia.Infrastructure.Transport.AspNetCore
//{
//	using Microsoft.AspNet.SignalR
//	using Microsoft.AspNetCore.Mvc
//	using Microsoft.AspNetCore.Mvc.Filters
//
//	public class RequiresAuthenticationTokenAttribute : ActionFilterAttribute
//    {
//        private readonly string[] _requiredRoles
//        private IHttpContextAuthenticationTokenVerifier _verifier
//	    //private readonly ISimpleAuthenticationTokenVerifier _simpleAuthenticationTokenVerifier
//
//        public RequiresAuthenticationTokenAttribute(params string[] requiredRoles)
//        {
//			_requiredRoles = requiredRoles
//        }
//
//	    public override void OnActionExecuting(ActionExecutingContext context)
//	    {
//		    // Delayed dependency injection.
//            // All other solutions are far slower.
//            if (_verifier == null)
//            {
//	            var dependencyResolver = (IDependencyResolver)context.HttpContext.RequestServices.GetService(typeof(IDependencyResolver))
//
//				_verifier = (IHttpContextAuthenticationTokenVerifier)dependencyResolver.GetService(typeof(IHttpContextAuthenticationTokenVerifier))
//            }
//			var status = _verifier.Verify(context.HttpContext, (Controller)context.Controller, _requiredRoles)
//            if (status is OkResult || 
//                status is UnauthorizedResult)
//            {
//                base.OnActionExecuting(context)
//            }
//        }
//
//    }
//}