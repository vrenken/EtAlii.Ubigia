//namespace EtAlii.Ubigia.Infrastructure.Transport.Grpc
//{
//	using System;
//	using System.Linq;
//	using System.Net;
//	using System.Security.Claims;
//	using System.Threading.Tasks;
//	using Microsoft.Grpc.Authorization;
//	using Microsoft.Grpc.Mvc;
//	using Microsoft.Grpc.Mvc.Filters;


//	internal class RolesRequirementHandler : AuthorizationHandler<RolesRequirement>, IAuthorizationRequirement
//	{
//		private readonly IHttpContextAuthenticationVerifier _verifier;

//		public RolesRequirementHandler(IHttpContextAuthenticationVerifier verifier)
//		{
//			_verifier = verifier;
//		}

//		protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, RolesRequirement requirement)
//		{
//			var status = _verifier.Verify(null, null, requirement.RequiredRoles);
//			if (status is OkResult)
//			{
//				context.Succeed(requirement);
//			}
//			else
//			{
//				context.Fail();
//			}

//			return Task.CompletedTask;
//		}
//	}
//}