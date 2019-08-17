namespace EtAlii.Ubigia.Infrastructure.Transport.NetCore
{
	using System.Threading.Tasks;
	using Microsoft.AspNetCore.Authorization;
	using Microsoft.AspNetCore.Mvc;

	internal class RolesRequirementHandler : AuthorizationHandler<RolesRequirement>, IAuthorizationRequirement
	{
		private readonly IHttpContextAuthenticationVerifier _verifier;

		public RolesRequirementHandler(IHttpContextAuthenticationVerifier verifier)
		{
			_verifier = verifier;
		}

		protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, RolesRequirement requirement)
		{
			var status = _verifier.Verify(null, null, requirement.RequiredRoles);
			if (status is OkResult)
			{
				context.Succeed(requirement);
			}
			else
			{
				context.Fail();
			}

			return Task.CompletedTask;
		}
	}
}