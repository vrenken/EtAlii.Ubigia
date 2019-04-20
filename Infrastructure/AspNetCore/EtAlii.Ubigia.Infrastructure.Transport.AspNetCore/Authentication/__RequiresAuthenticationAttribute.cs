//namespace EtAlii.Ubigia.Infrastructure.Transport.AspNetCore
//[
//	using System
//	using Microsoft.AspNet.SignalR
//	using Microsoft.AspNetCore.Mvc
//    using Microsoft.AspNetCore.Mvc.Filters
//	using AuthorizeAttribute = Microsoft.AspNetCore.Authorization.AuthorizeAttribute
//
//	/// <summary>
//	/// Generic Basic Authentication filter that checks for basic authentication
//	/// headers and challenges for authentication if no authentication is provided
//	/// Sets the Thread Principle with a GenericAuthenticationPrincipal.
//	/// 
//	/// You can override the OnAuthorize method for custom auth logic that
//	/// might be application specific.    
//	/// </summary>
//	/// <remarks>Always remember that Basic Authentication passes accountname and passwords
//	/// from client to server in plain text, so make sure SSL is used with basic auth
//	/// to encode the Authorization header on all requests (not just the login).
//	/// </remarks>
//	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
//    internal class RequiresAuthenticationAttribute : AuthorizeAttribute, IAuthorizationFilter
//	[
//	    private readonly string[] _requiredRoles
//
//	    public RequiresAuthenticationAttribute(params string[] requiredRoles)
//	    [
//		    _requiredRoles = requiredRoles
//	    ]
//
//		private IHttpContextAuthenticationVerifier _verifier
//
//		/// <summary>
//		/// Override to Web API filter method to handle Basic Auth check
//		/// </summary>
//		/// <param name="context"></param>
//		public void OnAuthorization(AuthorizationFilterContext context)
//		[
//            // Delayed dependency injection.
//            // All other solutions are far slower.
//            if [_verifier == null]
//            [
//	            var dependencyResolver = (IDependencyResolver)context.HttpContext.RequestServices.GetService(typeof(IDependencyResolver))
//	            _verifier = (IHttpContextAuthenticationVerifier)dependencyResolver.GetService(typeof(IHttpContextAuthenticationVerifier))
//				//_verifier = (IAuthenticationVerifier)actionContext.ControllerContext.Configuration.DependencyResolver.GetService(typeof(IAuthenticationVerifier))
//            ]
//
//			Controller controller = null
//            var status = _verifier.Verify(context.HttpContext, controller, _requiredRoles)
//            if [status is OkObjectResult]
//            [
//            ]
//            else
//            [
//	            context.Result = new UnauthorizedResult()
//            ]
//        ]
//
//    ]
//]