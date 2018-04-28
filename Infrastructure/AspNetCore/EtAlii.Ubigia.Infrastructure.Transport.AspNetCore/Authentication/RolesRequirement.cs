namespace EtAlii.Ubigia.Infrastructure.Transport.AspNetCore
{
	using Microsoft.AspNetCore.Authorization;


	internal class RolesRequirement : IAuthorizationRequirement
	{
		public readonly string[] RequiredRoles;

		public RolesRequirement(string[] requiredRoles)
		{
			RequiredRoles = requiredRoles;
		}
	}
}